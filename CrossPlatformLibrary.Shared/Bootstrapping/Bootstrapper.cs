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
using CommonServiceLocator;
using Tracing;

namespace CrossPlatformLibrary.Bootstrapping
{
    /// <summary>
    /// Provides an base implementation of <see cref="IBootstrapper"/> which has to be used to startup and shutdown
    /// your application. The startup sequence contains some virtual method calls which can be overriden by your own
    /// implementation of  <see cref="Bootstrapper"/>.
    /// 
    /// This <see cref="Bootstrapper"/> uses <see cref="SimpleIoc"/> as IoC container.
    /// </summary>
    public class Bootstrapper : Bootstrapper<SimpleIoc>
    {
    }

    /// <summary>
    /// Provides an base implementation of <see cref="IBootstrapper"/> which has to be used to startup and shutdown
    /// your application. The startup sequence contains some virtual method calls which can be overriden by your own
    /// implementation of  <see cref="Bootstrapper"/>.
    /// 
    /// This <see cref="Bootstrapper{TIocContainer}"/> takes <typeparam name="TIocContainer">a generic IIocContainer</typeparam> as IoC container.
    /// </summary>
    public class Bootstrapper<TIocContainer> : IBootstrapper where TIocContainer : class, IIocContainer
    {
        private ITracer tracer;

        /// <summary>
        /// Gets the IOC/DI container of the application, which is being
        /// created as part of the application initialization process.
        /// </summary>
        private TIocContainer iocContainer;

        private static ApplicationLifecycle applicationLifecycle = ApplicationLifecycle.Uninitialized;

        public Bootstrapper()
        {
            this.tracer = Tracer.Create(this);
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
        /// Returns the IoC container to be used for dependency management.
        /// </summary>
        protected virtual TIocContainer GetIocContainer()
        {
            return SimpleIoc.Default as TIocContainer;
        }

        /// <inheritdoc />
        public void Startup()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            this.tracer.Debug("Bootstrapper.Startup() called");

            this.iocContainer = this.GetIocContainer();

            // The Service container is a service locator too. To be backwards compatible set the ServiceLocator property.
            ServiceLocator.SetLocatorProvider(() => this.iocContainer);

            if (applicationLifecycle == ApplicationLifecycle.Uninitialized)
            {
                this.iocContainer.Reset();

                this.InternalConfigureTracing();

                this.InternalConfigureExceptionHandling();

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
        /// </summary>
        private void InternalConfigureTracing()
        {
            try
            {
                this.ConfigureTracing(this.iocContainer);
                this.tracer = Tracer.Create(this);
            }
            catch (Exception ex)
            {
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Bootstrapping failed during ConfigureDefaultTracerFactory sequence.", ex);
                }
            }
        }

        /// <summary>
        /// ConfigureTracing is called at the earliest possible point in time to configure the Tracer.
        /// By default, a platform-matching console tracer is configured.
        /// </summary>
        protected virtual void ConfigureTracing(TIocContainer container) 
        {
            if (container is ISimpleIoc simpleIoc)
            {
                // Register ITracer with a factory that allows type-specific tracer creation
                simpleIoc.Register<ITracer>((Type parentType) => Tracer.Create(parentType));
            }
        }

        private void InternalConfigureExceptionHandling()
        {
            try
            {
                // Register IExceptionHandlingStrategy
                var strategyType = this.GetExceptionHandlingStrategyType() ?? this.GetDefaultExceptionHandlingStrategyType();
                this.iocContainer.RegisterSingleton<IExceptionHandlingStrategy>(strategyType);

                // Register the platform-specific exception handler which injects IExceptionHandlingStrategy
                this.iocContainer.RegisterSingleton<IExceptionHandler, ExceptionHandler>();

                this.iocContainer.Update();
                this.iocContainer.GetInstance<IExceptionHandler>();
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
        /// A handler for bootstrapping errors occurring during startup and run.
        /// </summary>
        /// <param name="ex">The exception that has occurred.</param>
        /// <returns>
        /// <c>True</c> if the exception was handled by the method and can be ignored by the caller of this method, <c>false</c> otherwise.
        /// </returns>
        protected virtual bool HandleBootstrappingException(Exception ex)
        {
            try
            {
                var exceptionHandlingStrategy = this.iocContainer.GetInstance<IExceptionHandlingStrategy>();

                var isExceptionHandled = exceptionHandlingStrategy != null && exceptionHandlingStrategy.HandleException(ex);
                if (!isExceptionHandled)
                {
                    this.tracer.Exception(ex, "BootstrapperUnhandledException");
                }
            }
            catch { }

            return false;
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

                this.iocContainer.RegisterSingleton<IDispatcherService, DispatcherService>();
                this.iocContainer.RegisterSingleton<ILocalizer, Localizer>();
                this.iocContainer.RegisterSingleton<IPlatformServices, PlatformServices>();
                this.iocContainer.Update();

                this.ConfigureContainer(this.iocContainer);
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
        /// ConfigureContainer is used to register services.
        /// After that the container is ready for service resolves.
        /// </summary>
        protected virtual void ConfigureContainer(TIocContainer container)
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
                this.iocContainer.Reset();
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