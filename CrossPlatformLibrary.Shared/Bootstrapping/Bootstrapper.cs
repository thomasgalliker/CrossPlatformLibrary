using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.ExceptionHandling.ExceptionHandlingStrategies;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Localization;
using CrossPlatformLibrary.Tools;
using Microsoft.Practices.ServiceLocation;
using Tracing;

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

        private static ApplicationLifecycle applicationLifecycle = ApplicationLifecycle.Uninitialized;

        public Bootstrapper()
        {
            this.tracer = Tracer.Create<Bootstrapper>();
        }

        public static ApplicationLifecycle ApplicationLifecycle
        {
            get
            {
                return applicationLifecycle;
            }
            internal set // Used for unit testing
            {
                applicationLifecycle = value;
            }
        }

        ApplicationLifecycle IBootstrapper.ApplicationLifecycle
        {
            get
            {
                return ApplicationLifecycle;
            }
        }

        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        public void Startup()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            this.tracer.Debug("Bootstrapper.Startup() called");

            this.simpleIoc = SimpleIoc.Default;

            // The Service container is a service locator too. To be backwards compatible set the ServiceLocator property.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (applicationLifecycle == ApplicationLifecycle.Uninitialized)
            {
                this.simpleIoc.Reset();

                this.InternalConfigureDefaultTracerFactory();

                this.InternalConfigureExceptionHandling();

                this.InternalConfigureExtensions();

                this.InternalConfigureContainer();

                this.InternalOnStartup();
            }
            else if (applicationLifecycle == ApplicationLifecycle.Sleep)
            {
                this.InternalOnResume();
            }
            else if(applicationLifecycle == ApplicationLifecycle.Running)
            {
                this.tracer.Info($"ApplicationLifecycle is already in state {ApplicationLifecycle.Running}.");
            }

            stopwatch.Stop();
            this.tracer.Debug($"Bootstrapper.Startup() finished in {stopwatch.Elapsed}.");
        }

        public void Sleep()
        {
            applicationLifecycle = ApplicationLifecycle.Sleep;
        }

        /// <summary>
        /// Configures a platform-specific default tracer factory.
        /// Each target platform can configure
        /// </summary>
        private void InternalConfigureDefaultTracerFactory()
        {
            try
            {
                // Register ITracer with a factory that allows type-specific tracer creation
                this.simpleIoc.Register<ITracer>((Type parentType) => Tracer.Create(parentType));
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureDefaultTracerFactory sequence.", ex);
                }
            }
        }

        private void InternalConfigureExceptionHandling()
        {
            try
            {
                // Register IExceptionHandlingStrategy
                var strategyType = this.GetExceptionHandlingStrategyType() ?? this.GetDefaultExceptionHandlingStrategyType();
                this.simpleIoc.Register<IExceptionHandlingStrategy>(strategyType);

                // Register the platform-specific exception handler which injects IExceptionHandlingStrategy
                this.simpleIoc.Register<IExceptionHandler, ExceptionHandler>();
                this.simpleIoc.GetInstance<IExceptionHandler>();
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

                this.simpleIoc.Register<IDispatcherService, DispatcherService>();
                this.simpleIoc.Register<ILocalizer, Localizer>();

                this.simpleIoc.Register<IPlatformServices, PlatformServices>();
                var platformServices = this.simpleIoc.GetInstance<IPlatformServices>();
                platformServices.LoadReferencedAssemblies();

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
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureExtensions sequence.", ex);
                }
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

        /// <summary>
        /// A handler for bootstrapping errors occurring during startup and run.
        /// </summary>
        /// <param name="ex">The exception that has occurred.</param>
        /// <returns>
        /// <c>True</c> if the exception was handled by the method and can be ignored by the caller of this method, <c>false</c> otherwise.
        /// </returns>
        protected virtual bool HandleBootstrappingException(Exception ex)
        {
            var exceptionHandlingStrategy = this.simpleIoc.TryGetInstance<IExceptionHandlingStrategy>();

            var isExceptionHandled = exceptionHandlingStrategy != null && exceptionHandlingStrategy.HandleException(ex);
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
        /// <remarks>When overridden by inheriting classes, this method must return a type which implements <see cref="IExceptionHandlingStrategy"/>.
        /// If this method returns <c>null</c>, the <see cref="TracingExceptionHandlingStrategy"/> is used as default.</remarks>
        protected virtual Type GetExceptionHandlingStrategyType()
        {
            return this.GetDefaultExceptionHandlingStrategyType();
        }

        private Type GetDefaultExceptionHandlingStrategyType()
        {
            return typeof(TracingExceptionHandlingStrategy);
        }

        private void InternalOnStartup()
        {
            try
            {
                this.tracer.Debug("Calling custom OnStartup procedure");
                this.OnStartup();
                applicationLifecycle = ApplicationLifecycle.Running;
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
        /// Does the actual start procedure for the application or service.
        /// </summary>
        /// <remarks>When implemented by inheriting classes, this method will show the shell form of the application or start the service.</remarks>
        protected virtual void OnStartup()
        {
        }

        public void Resume()
        {
            this.InternalOnResume();
        }

        private void InternalOnResume()
        {
            try
            {
                this.tracer.Debug("Calling custom OnResume procedure");
                this.OnResume();
                applicationLifecycle = ApplicationLifecycle.Running;
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during custom OnResume sequence.", ex);
                }
            }
        }

        protected virtual void OnResume()
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
                applicationLifecycle = ApplicationLifecycle.Uninitialized;
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

        ~Bootstrapper()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Shutdown();
            }
        }
    }
}