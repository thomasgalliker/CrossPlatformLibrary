using System;
using System.Linq;
using System.Reflection;

using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.ExceptionHandling.Handlers;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tools;
using CrossPlatformLibrary.Tracing;

using Microsoft.Practices.ServiceLocation;

namespace CrossPlatformLibrary.Bootstrapping
{
    /// <summary>
    /// Provides an abstract base implementation of <see cref="IBootstrapper"/> 
    /// that will use an <see cref="IServiceLocator"/> as its configuration container.
    /// </summary>
    public abstract class BootstrapperBase : IBootstrapper
    {
        private readonly ITracer tracer;

        /// <summary>
        /// Gets the IOC/DI container of the application, which is being
        /// created as part of the application initialization process.
        /// </summary>
        private SimpleIoc simpleIoc;

        protected BootstrapperBase(ITracer tracer = null)
        {
            this.tracer = tracer ?? Tracer.Create(this);
        }

        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// 1) Set ServiceLocator
        /// 2) Register ITracer interface in IoC
        /// 3) Initialize all IContainerExtension implementations
        /// 4) Register and instantiate IExceptionHandler
        /// 5) ConfigureContainer (virtual method)
        /// 6) OnStartup (abstract method)
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        public void Startup()
        {
            this.tracer.Debug("Startup");

            this.simpleIoc = SimpleIoc.Default;

            // The Service container is a service locator too. To be backwards compatible set the ServiceLocator property.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            this.simpleIoc.Register<ITracer>((Type parentType) => Tracer.Create(parentType));

            this.ConfigureExtensions();

            // Register the default exception handler and instantiate it immediately
            var exceptionHandlerType = this.GetExceptionHandlerType() ?? this.GetDefaultExceptionHandlerType();
            this.simpleIoc.Register<IExceptionHandler>(exceptionHandlerType, true);

            this.InternalConfigureContainer();

            this.InternalOnStartup();
        }

        /// <summary>
        /// Configures all extension which implement the IContainerExtension interface
        /// </summary>
        private void ConfigureExtensions()
        {
            try
            {
                this.tracer.Debug("Calling ConfigureExtensions procedure");

                // Try to find all known types which implement the IContainerExtension interface
                var containerExtensionInterface = typeof(IContainerExtension);
                var allAssemblies = PlatformServices.GetAssemblies();
                var containerExtensionTypes = allAssemblies.SelectMany(a => a.ExportedTypes)
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
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureExtensions sequence.", ex);
                }
            }
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
            var exceptionHandler = this.simpleIoc.GetInstance<IExceptionHandler>();

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
        protected abstract void OnStartup();

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