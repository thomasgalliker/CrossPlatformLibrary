using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.ExceptionHandling.Handlers;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tools;
using CrossPlatformLibrary.Tracing;

using Microsoft.Practices.ServiceLocation;

namespace CrossPlatformLibrary.Bootstrapping
{
    /// <summary>
    /// Provides an base implementation of <see cref="IBootstrapper"/> which has to be used to startup and shutdown
    /// your application. The startup sequence contains some virtual method calls which can be overriden by your own
    /// implementation of  <see cref="Bootstrapper"/>.
    /// </summary>
    public class Bootstrapper : IBootstrapper
    {
        private readonly ITracer tracer;

        /// <summary>
        /// Gets the IOC/DI container of the application, which is being
        /// created as part of the application initialization process.
        /// </summary>
        private SimpleIoc simpleIoc;

        public Bootstrapper(ITracer tracer = null)
        {
            this.tracer = tracer ?? Tracer.Create(this);
        }

        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        public void Startup()
        {
            this.tracer.Debug("Startup");

            this.simpleIoc = SimpleIoc.Default;

            // The Service container is a service locator too. To be backwards compatible set the ServiceLocator property.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register ITracer with a factory that allows type-specific tracer creation
            this.simpleIoc.Register<ITracer>((Type parentType) => Tracer.Create(parentType));

            this.InternalConfigureExceptionHandling();

            this.InternalConfigureExtensions();

            this.InternalConfigureContainer();

            this.InternalOnStartup();
        }

        private void InternalConfigureExceptionHandling()
        {
            try
            {
                // Register the default exception handler and instantiate it immediately
                this.simpleIoc.RegisterWithConvention<IPlatformSpecificExceptionHandler>();
                var exceptionHandlerType = this.GetExceptionHandlerType() ?? this.GetDefaultExceptionHandlerType();
                this.simpleIoc.Register<IExceptionHandler>(exceptionHandlerType);

                // Wire-up IPlatformSpecificExceptionHandler with specified IExceptionHandler 
                var exceptionHandler = this.simpleIoc.GetInstance<IExceptionHandler>();
                var platformSpecificExceptionHandler = this.simpleIoc.GetInstance<IPlatformSpecificExceptionHandler>();
                platformSpecificExceptionHandler.RegisterExceptionHandler(exceptionHandler);
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureExceptionHandling sequence.", ex);
                }
            }
        }

        /// <summary>
        /// Make sure all plugin assemblies are loaded and configured accordingly.
        /// </summary>
        private void InternalConfigureExtensions()
        {
            try
            {
                this.tracer.Debug("Calling ConfigureExtensions procedure");

                if (this.ConfigureExtensionAssemblyFilter() != null)
                {
                    CrossPlatformLibrary.AssemblyNamespaces.AddRange(this.ConfigureExtensionAssemblyFilter());
                }

                this.simpleIoc.RegisterWithConvention<IPlatformServices>();
                var platformServices = this.simpleIoc.GetInstance<IPlatformServices>();
                platformServices.LoadReferencedAssemblies();

                this.ConfigureExtensions(platformServices);

            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureExtensions sequence.", ex);
                }
            }
        }

        /// <summary>
        /// Returns a list of assembly names which shall be loaded at startup time.
        /// The given assemblies may contain plugins which can be configured at boostrapping time using IContainerExtension interface.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<string> ConfigureExtensionAssemblyFilter()
        {
            return null;
        }

        /// <summary>
        /// Configures all extension which implement the IContainerExtension interface
        /// </summary>
        private void ConfigureExtensions(IPlatformServices platformServices)
        {
            // Try to find all known types which implement the IContainerExtension interface
            var containerExtensionInterface = typeof(IContainerExtension);
            var allAssemblies = platformServices.GetAssemblies();
            var containerExtensionTypes = allAssemblies
                .Where(a => a.FullName.ContainsAny(CrossPlatformLibrary.AssemblyNamespaces))
                .SelectMany(this.TryGetExportedTypes)
                .Distinct()
                .Where(t => containerExtensionInterface.GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                .Where(t => t != containerExtensionInterface)
                .ToList();

            // Use the dependency service to create IContainerExtension-based objects
            // and call Initialize in order to hand-over the dependency service to the modules
            foreach (var containerExtensionType in containerExtensionTypes)
            {
                string containerExtensionIdentifier = containerExtensionType.FullName;

                this.simpleIoc.Register(containerExtensionType, containerExtensionIdentifier, false);
                var containerExtension = (IContainerExtension)this.simpleIoc.GetInstanceWithoutCaching(containerExtensionType, containerExtensionIdentifier);

                this.tracer.Debug("Initializing container extension {0}.", containerExtension.GetType().FullName);
                containerExtension.Initialize(this.simpleIoc);

                this.simpleIoc.Unregister(containerExtensionType, containerExtensionIdentifier);
            }
        }

        private IEnumerable<Type> TryGetExportedTypes(Assembly assembly)
        {
            try
            {
                if (assembly.DefinedTypes.Any())
                {
                    return assembly.ExportedTypes;
                }
            }
            catch (Exception ex)
            {
                this.tracer.Error("Assembly {0} could not access ExportedTypes: {1}", assembly.FullName, ex.Message);
            }

            return Enumerable.Empty<Type>();
        }

        private void InternalOnStartup()
        {
            try
            {
                this.tracer.Debug("Calling custom OnStartup procedure");
                this.OnStartup();
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during custom OnStratup sequence.", ex);
                }
            }
        }

        /// <summary>
        /// A handler for bootstrapping errors occurring during startup and run.
        /// </summary>
        /// <param name="ex">The exception that has occurred.</param>
        /// <returns>
        /// <c>True</c> if the exception was handled by the method and can be ignored by the caller of this method, <c>false</c> otherwise.
        /// </returns>
        protected virtual bool HandleBootstrappingException(Exception ex)
        {
            var exceptionHandler = this.simpleIoc.TryGetInstance<IExceptionHandler>();

            var isExceptionHandled = exceptionHandler != null && exceptionHandler.HandleException(ex);
            if (!isExceptionHandled)
            {
                this.tracer.Exception(ex, "BootstrapperUnhandledException");
            }

            return isExceptionHandled;
        }

        /// <summary>
        /// The purpose of the instance which will be created from the given type is to handle any <see cref="Exception"/>
        /// which is not handled by the application.
        /// </summary>
        /// <remarks>When overridden by inheriting classes, this method must return a type which implements <see cref="IExceptionHandler"/>.
        /// If this method returns <c>null</c>, the <see cref="RethrowExceptionHandler"/> is used as default.</remarks>
        protected virtual Type GetExceptionHandlerType()
        {
            return this.GetDefaultExceptionHandlerType();
        }

        private Type GetDefaultExceptionHandlerType()
        {
            return typeof(RethrowExceptionHandler);
        }

        /// <summary>
        /// Does the actual start procedure for the application or service.
        /// </summary>
        /// <remarks>When implemented by inheriting classes, this method will show the shell form of the application or start the service.</remarks>
        protected virtual void OnStartup()
        {
        }

        private void InternalConfigureContainer()
        {
            try
            {
                this.tracer.Debug("Calling custom ConfigureContainer procedure");
                this.ConfigureContainer(this.simpleIoc);
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during custom ConfigureContainer sequence.", ex);
                }
            }
        }

        /// <summary>
        /// ConfigureContainer is called when all necessary dependencies are registered.
        /// ConfigureContainer is intended to resolve dependencies and configure them before first use.
        /// </summary>
        protected virtual void ConfigureContainer(ISimpleIoc container)
        {
        }

        public void Shutdown()
        {
            try
            {
                this.tracer.Debug("Calling custom OnShutdown procedure");
                this.OnShutdown();
            }
            catch (Exception ex)
            {
                this.tracer.Debug("Custom OnShutdown procedure failed");
                if (!this.HandleBootstrappingException(ex))
                {
                    throw;
                }
            }
            finally
            {
                if (this.simpleIoc != null)
                {
                    this.simpleIoc.Reset();
                    this.simpleIoc = null;
                }
            }
        }

        /// <summary>
        /// Does the actual shutdown procedure for the application or service.
        /// </summary>
        /// <remarks>When overridden by inheriting classes, this method will close the shell form of the application or stop the service.</remarks>
        protected virtual void OnShutdown()
        {
        }
    }
}