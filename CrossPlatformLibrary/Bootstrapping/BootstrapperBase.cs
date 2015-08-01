using System;

using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tools.PlatformSpecific;
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
        private ISimpleIoc container;

        protected BootstrapperBase()
        {
            this.tracer = Tracer.Create(this);
        }

        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        public void Startup()
        {
            this.tracer.Debug("Startup");

            this.container = SimpleIoc.Default;
            
            // The Service container is a service locator too. To be backwards
            // compatible set the ServiceLocator property.
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register the default exception handler
            var exceptionHandler = this.CreateExceptionHandler();
            this.container.Register<IExceptionHandler>(() => exceptionHandler);

            // Register services delivered in base library
            var dispatcherService = PlatformAdapter.Resolve<IDispatcherService>();
            this.container.Register<IDispatcherService>(() => dispatcherService);
            
            this.container.Register<ITracer>((Type parentType) => Tracer.Create(parentType));

            try
            {
                this.tracer.Debug("Calling custom OnStartup procedure");
                this.OnStartup();
            }
            catch (Exception ex)
            {
                this.tracer.Debug("Custom OnStartup procedure failed");
                if (!this.HandleBootstrappingException(ex))
                {
                    throw new BootstrappingException("Failed during custom OnStratup sequence.", ex);
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
            var exceptionHandler = this.container.GetInstance<IExceptionHandler>();

            var isExceptionHandled = exceptionHandler != null && exceptionHandler.HandleException(ex);
            if (!isExceptionHandled)
            {
                this.tracer.Exception(ex, "BootstrapperUnhandledExceptionStartup");
            }

            return isExceptionHandled;
        }

        /// <summary>
        /// Runs the shutdown procedure of the bootstrapper.
        /// </summary>
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
                if (this.container != null)
                {
                    this.container.Reset();
                    this.container = null;
                }
            }
        }

        /// <summary>
        /// This <see cref="IExceptionHandler"/> instance can be used by other components to handle any <see cref="Exception"/> that is
        /// not handled by the application.
        /// </summary>
        /// <remarks>When overridden by inheriting classes, this method must return a new instance of a <see cref="IExceptionHandler"/>.
        /// If this method returns <c>null</c>, the <see cref="RethrowExceptionHandler"/> is used as default.</remarks>
        protected virtual IExceptionHandler CreateExceptionHandler()
        {
            return new RethrowExceptionHandler();
        }

        /// <summary>
        /// Does the actual start procedure for the application or service.
        /// </summary>
        /// <remarks>When implemented by inheriting classes, this method will show the shell form of the application or start the service.</remarks>
        protected abstract void OnStartup();

        /// <summary>
        /// Does the actual shutdown procedure for the application or service.
        /// </summary>
        /// <remarks>When overridden by inheriting classes, this method will close the shell form of the application or stop the service.</remarks>
        protected virtual void OnShutdown()
        {
        }
    }
}