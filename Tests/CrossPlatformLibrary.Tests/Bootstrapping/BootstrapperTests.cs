using System;
using System.Threading;

using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;
using CrossPlatformLibrary.Tools.PlatformSpecific;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Bootstrapping
{
    [Collection("Bootstrapping")]
    public class BootstrapperTests
    {
        public BootstrapperTests()
        {
            SimpleIoc.Default.Reset();
            Bootstrapper.ApplicationLifecycle = ApplicationLifecycle.Uninitialized;
        }

        [Fact]
        public void ShouldStartupBootstrapper()
        {
            // Arrange
            var testRegistrationConvention = new TestRegistrationConvention();
            SimpleIoc.Default.SetAdapterResolver(new ProbingAdapterResolver(testRegistrationConvention));
            IBootstrapper bootstrapper = new Bootstrapper();

            // Act
            bootstrapper.Startup();

            // Assert
            bootstrapper.ApplicationLifecycle.Should().Be(ApplicationLifecycle.Running);
        }

        [Fact]
        public void ShouldShutdownBootstrapper()
        {
            // Arrange
            var testRegistrationConvention = new TestRegistrationConvention();
            SimpleIoc.Default.SetAdapterResolver(new ProbingAdapterResolver(testRegistrationConvention));
            IBootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            // Act
            bootstrapper.Shutdown();

            // Assert
            bootstrapper.ApplicationLifecycle.Should().Be(ApplicationLifecycle.Uninitialized);
        }

        [Fact]
        public void ShouldSleepBootstrapper()
        {
            // Arrange
            var testRegistrationConvention = new TestRegistrationConvention();
            SimpleIoc.Default.SetAdapterResolver(new ProbingAdapterResolver(testRegistrationConvention));
            IBootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            // Act
            bootstrapper.Sleep();

            // Assert
            bootstrapper.ApplicationLifecycle.Should().Be(ApplicationLifecycle.Sleep);
        }

        [Fact]
        public void ShouldResumeBootstrapper()
        {
            // Arrange
            var testRegistrationConvention = new TestRegistrationConvention();
            SimpleIoc.Default.SetAdapterResolver(new ProbingAdapterResolver(testRegistrationConvention));
            IBootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Startup();
            bootstrapper.Sleep();

            // Act
            bootstrapper.Resume();

            // Assert
            bootstrapper.ApplicationLifecycle.Should().Be(ApplicationLifecycle.Running);
        }

        [Fact]
        public void ShouldConfigureExtensionAssemblyFilter()
        {
            ////var bootstrapperMock = new Mock<Bootstrapper>();
            
            ////bootstrapperMock.Protected().Setup<IEnumerable<string>>("ConfigureExtensionAssemblyFilter").Returns(() => new List<string> { "abdbdbd" });
            ////bootstrapperMock.CallBase = true;
            ////bootstrapperMock.Object.Startup();
        }

        //[Fact]
        //public void ConfigureModulesRunsModuleManager()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IModuleManager> managerMock = mocks.Create<IModuleManager>();

        //    IUnityContainer container = new UnityContainer();
        //    container.RegisterInstance<IModuleManager>(managerMock.Object);
        //    managerMock.Setup(manager => manager.Run());

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.ConfigureModulesMethod(container);

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void ConfigureModulesThrowsIfNotModuleManagerIsRegistered()
        //{
        //    IUnityContainer container = new UnityContainer();
        //    BootstrapperMock bootstrapper = new BootstrapperMock();

        //    ExceptionAssert.IsThrown<BootstrappingException>(() => bootstrapper.ConfigureModulesMethod(container));
        //}

        //[Fact]
        //public void ConfigureModulesThrowsIfContainerIsNull()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    ExceptionAssert.IsThrown<ArgumentNullException>(() => bootstrapper.ConfigureModulesMethod(null));
        //}

        //[Fact]
        //public void StartupThrowsIfConfigureModulesThrows()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.ConfigureModulesAction = arg => { throw new ModularityException(); };
        //    ExceptionAssert.IsThrown<BootstrappingException>(() => bootstrapper.Startup());
        //}

        //[Fact]
        //public void StartupCallsExceptionHandlerIfRunThrows()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IExceptionHandler> exceptionHandlerMock = mocks.Create<IExceptionHandler>();

        //    Exception thrownException = new Exception();

        //    exceptionHandlerMock.Setup(exceptionHandler => exceptionHandler.HandleException(thrownException)).Returns(true);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CreateExceptionHandlerAction = () => exceptionHandlerMock.Object;
        //    bootstrapper.OnStartupAction = () => { throw thrownException; };

        //    bootstrapper.Startup();

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupRethrowsIfExceptionHandlerNotHandlesExceptionThrownInRun()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IExceptionHandler> exceptionHandlerMock = mocks.Create<IExceptionHandler>();

        //    Exception thrownException = new Exception();

        //    exceptionHandlerMock.Setup(exceptionHandler => exceptionHandler.HandleException(thrownException)).Returns(false);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CreateExceptionHandlerAction = () => exceptionHandlerMock.Object;
        //    bootstrapper.OnStartupAction = () => { throw thrownException; };

        //    Exception caughtException = ExceptionAssert.IsThrown<Exception>(() => bootstrapper.Startup());
        //    Assert.AreSame(thrownException, caughtException);
        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupRegistersSystemConfigurationSourceIfCollectConfigurationReturnsNull()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.Startup();

        //    IConfigurationSource registeredConfiguration = bootstrapper.ContainerProperty.Resolve<IConfigurationSource>();
        //    Assert.IsNotNull(registeredConfiguration);
        //    Assert.IsInstanceOfType(registeredConfiguration, typeof(SystemConfigurationSource));
        //}

        //[Fact]
        //public void StartupRegistersConfigurationSourceReturnedByCollectConfiguration()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    IConfigurationSource registeredConfiguration = bootstrapper.ContainerProperty.Resolve<IConfigurationSource>();
        //    Assert.AreSame(configurationMock.Object, registeredConfiguration);

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupAddConfigurationResolutionExtensionToTheContainer()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.Startup();

        //    ConfigurationResolution extension = bootstrapper.ContainerProperty.Configure<ConfigurationResolution>();
        //    Assert.IsNotNull(extension);
        //    Assert.IsInstanceOfType(extension.Configuration, typeof(SystemConfigurationSource));
        //}

        //[Fact]
        //public void StartupAddConfigurationResolutionExtensionToTheContainerWithCollectedConfiguration()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    ConfigurationResolution extension = bootstrapper.ContainerProperty.Configure<ConfigurationResolution>();
        //    Assert.IsNotNull(extension);
        //    Assert.AreSame(configurationMock.Object, extension.Configuration);

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupUsesUnityConfigurationIfExist()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => new FileConfigurationSource("Roche.LabCore.Bootstrapping\\Unity.config");
        //    bootstrapper.Startup();

        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsRegistered<IComponent>(typeof(TransientLifetimeManager)));
        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsMappingRegistered<IComponent, Component>(typeof(TransientLifetimeManager)));
        //}

        //[Fact]
        //public void StartupAddConfiguredTracerFactoryToTheContainer()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    TracingSection section = new TracingSection();
        //    section.TracerFactory.ConfiguredType = typeof(Log4NetTracerFactory);

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(section);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection("log4net")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsRegistered<ITracerFactory>(typeof(ContainerControlledLifetimeManager)));
        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsMappingRegistered<ITracerFactory, Log4NetTracerFactory>(typeof(ContainerControlledLifetimeManager)));

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupAddDefaultTracerFactoryToTheContainerIfNoIsConfigured()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsRegistered<ITracerFactory>(typeof(ContainerControlledLifetimeManager)));
        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsMappingRegistered<ITracerFactory, EmptyTracerFactory>(typeof(ContainerControlledLifetimeManager)));

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupAddConfiguredTraceReaderToTheContainer()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    TracingSection section = new TracingSection();
        //    section.TraceReader.ConfiguredType = typeof(Log4NetTraceReader);

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(section);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsRegistered<ITraceReader>(typeof(ContainerControlledLifetimeManager)));
        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsMappingRegistered<ITraceReader, Log4NetTraceReader>(typeof(ContainerControlledLifetimeManager)));

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupAddDefaultTraceReaderToTheContainerIfNoIsConfigured()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsRegistered<ITraceReader>(typeof(ContainerControlledLifetimeManager)));
        //    Assert.IsTrue(bootstrapper.ContainerProperty.IsMappingRegistered<ITraceReader, EmptyTraceReader>(typeof(ContainerControlledLifetimeManager)));

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupAddTracerCreationExtensionToTheContainer()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IConfigurationSource> configurationMock = mocks.Create<IConfigurationSource>();

        //    configurationMock.Setup(configuration => configuration.GetSection<UnityConfigurationSection>("unity")).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<TracingSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<FilterSection>()).Returns(() => null);
        //    configurationMock.Setup(configuration => configuration.GetSection<LabCoreSection>()).Returns(() => null);

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () => configurationMock.Object;
        //    bootstrapper.Startup();

        //    TracerCreation extension = bootstrapper.ContainerProperty.Configure<TracerCreation>();
        //    Assert.IsNotNull(extension);
        //    Assert.IsInstanceOfType(extension.Factory, typeof(EmptyTracerFactory));

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupRegistersRethrowExceptionHandlerIfCreateExceptionHandlerReturnsNull()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.Startup();

        //    IExceptionHandler registeredExceptionHandler = bootstrapper.ContainerProperty.Resolve<IExceptionHandler>();
        //    Assert.IsNotNull(registeredExceptionHandler);
        //    Assert.IsInstanceOfType(registeredExceptionHandler, typeof(RethrowExceptionHandler));
        //}

        //[Fact]
        //public void StartupRegistersExceptionHandlerReturnedByCreateExceptionHandler()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IExceptionHandler> exceptionHandlerMock = mocks.Create<IExceptionHandler>();

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CreateExceptionHandlerAction = () => exceptionHandlerMock.Object;
        //    bootstrapper.Startup();

        //    IExceptionHandler registeredExceptionHandler = bootstrapper.ContainerProperty.Resolve<IExceptionHandler>();
        //    Assert.AreSame(exceptionHandlerMock.Object, registeredExceptionHandler);

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupRegistersEmptyModuleCatalogIfCollectModulesReturnsNull()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.Startup();

        //    IModuleCatalog registeredCatalog = bootstrapper.ContainerProperty.Resolve<IModuleCatalog>();
        //    Assert.IsNotNull(registeredCatalog);
        //    Assert.IsInstanceOfType(registeredCatalog, typeof(EmptyModuleCatalog));
        //}

        //[Fact]
        //public void StartupRegistersModuleCatalogReturnedByCollectModules()
        //{
        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IModuleCatalog> catalogMock = mocks.Create<IModuleCatalog>();

        //    catalogMock.Setup(catalog => catalog.RetrieveCatalogItems()).Returns(new ModuleCatalogItem[] { });

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectModulesAction = () => catalogMock.Object;
        //    bootstrapper.Startup();

        //    IModuleCatalog registeredCatalog = bootstrapper.ContainerProperty.Resolve<IModuleCatalog>();
        //    Assert.AreSame(catalogMock.Object, registeredCatalog);

        //    mocks.VerifyAll();
        //}

        //[Fact]
        //public void StartupCallsConfigureContainerWithCorrectArguments()
        //{
        //    IUnityContainer containerArgument = null;

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.ConfigureContainerAction = arg =>
        //        {
        //            containerArgument = arg;
        //            bootstrapper.ConfigureContainerMethod(arg);
        //        };
        //    bootstrapper.Startup();

        //    Assert.AreSame(bootstrapper.ContainerProperty, containerArgument);
        //}

        //[Fact]
        //public void StartupCallsConfigureModulesWithCorrectArguments()
        //{
        //    IUnityContainer containerArgument = null;

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.ConfigureModulesAction = arg =>
        //        {
        //            containerArgument = arg;
        //            bootstrapper.ConfigureModulesMethod(arg);
        //        };
        //    bootstrapper.Startup();

        //    Assert.AreSame(bootstrapper.ContainerProperty, containerArgument);
        //}

        //[Fact]
        //public void StartupCallsMethodsInTheCorrectOrder()
        //{
        //    int count = 0;
        //    int collectConfigurationIndex = 0;
        //    int createExceptionHandlerIndex = 0;
        //    int collectModulesIndex = 0;
        //    int configureContainerIndex = 0;
        //    int configureModulesIndex = 0;
        //    int runIndex = 0;

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.CollectConfigurationAction = () =>
        //        {
        //            count++;
        //            collectConfigurationIndex = count;
        //            return null;
        //        };
        //    bootstrapper.CreateExceptionHandlerAction = () =>
        //        {
        //            count++;
        //            createExceptionHandlerIndex = count;
        //            return null;
        //        };
        //    bootstrapper.CollectModulesAction = () =>
        //        {
        //            count++;
        //            collectModulesIndex = count;
        //            return null;
        //        };
        //    bootstrapper.ConfigureContainerAction = container =>
        //        {
        //            count++;
        //            configureContainerIndex = count;
        //            bootstrapper.ConfigureContainerMethod(container);
        //        };
        //    bootstrapper.ConfigureModulesAction = container =>
        //        {
        //            count++;
        //            configureModulesIndex = count;
        //            bootstrapper.ConfigureModulesMethod(container);
        //        };
        //    bootstrapper.OnStartupAction = () =>
        //        {
        //            count++;
        //            runIndex = count;
        //        };

        //    bootstrapper.Startup();

        //    Assert.AreEqual(1, collectConfigurationIndex);
        //    Assert.AreEqual(2, createExceptionHandlerIndex);
        //    Assert.AreEqual(3, collectModulesIndex);
        //    Assert.AreEqual(4, configureContainerIndex);
        //    Assert.AreEqual(5, configureModulesIndex);
        //    Assert.AreEqual(6, runIndex);
        //}

        //[Fact]
        //public void ShutdownDisposeContainer()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();

        //    bootstrapper.Startup();
        //    bootstrapper.Shutdown();

        //    Assert.IsNull(bootstrapper.ContainerProperty);
        //}

        //[Fact]
        //public void ShutdownCallsMethodsInTheCorrectOrder()
        //{
        //    int count = 0;
        //    int closeIndex = 0;

        //    BootstrapperMock bootstrapper = new BootstrapperMock();
        //    bootstrapper.OnShutdownAction = () =>
        //        {
        //            count++;
        //            closeIndex = count;
        //            bootstrapper.CloseMethod();
        //        };

        //    bootstrapper.Startup();
        //    bootstrapper.Shutdown();

        //    Assert.AreEqual(1, closeIndex);
        //}

        //[Fact]
        //public void BootstrapperStartsActiveModules()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();


        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IModuleManager> managerMock = mocks.Create<IModuleManager>();

        //    IUnityContainer container = new UnityContainer();
        //    container.RegisterInstance<IModuleManager>(managerMock.Object);

        //    var record = new Mock<IRecord>(MockBehavior.Strict);

        //    record.Setup(s => s.Report("ModuleConfigure@1", It.IsAny<bool>(), It.IsAny<bool>()));
        //    record.Setup(s => s.Report("Startup@2", It.IsAny<bool>(), It.IsAny<bool>()));
        //    record.Setup(s => s.Report("Run@3", It.IsAny<bool>(), It.IsAny<bool>()));
        //    record.Setup(s => s.Report("Close@5", It.IsAny<bool>(), It.IsAny<bool>()));
        //    record.Setup(s => s.Report("Shutdown@6", It.IsAny<bool>(), It.IsAny<bool>()));

        //    var runContext = new TestSynchronizationContext();
        //    var startupContext = new TestSynchronizationContext();

        //    int i = 1;
        //    bootstrapper.ConfigureModulesAction = m => record.Object.Report("ModuleConfigure@" + (i++).ToString(), runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.StartupActiveModuleAction = m => record.Object.Report("Startup@" + (i++).ToString(), runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.ShutdownActiveModuleAction = m => record.Object.Report("Shutdown@" + (i++).ToString(), runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.OnShutdownAction = () => record.Object.Report("Close@" + (i++).ToString(), runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.OnStartupAction = () => record.Object.Report("Run@" + (i++).ToString(), runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.GetRunSyncContext = () => runContext;
        //    bootstrapper.GetStartupSyncContext = () => startupContext;

        //    bootstrapper.Startup();

        //    i++;
            
        //    bootstrapper.Shutdown();

        //    record.VerifyAll();
        //}

        //[Fact]
        //public void BootstrapperSynchronizationContext()
        //{
        //    BootstrapperMock bootstrapper = new BootstrapperMock();


        //    MockRepository mocks = new MockRepository(MockBehavior.Strict);
        //    Mock<IModuleManager> managerMock = mocks.Create<IModuleManager>();

        //    IUnityContainer container = new UnityContainer();
        //    container.RegisterInstance<IModuleManager>(managerMock.Object);

        //    var record = new Mock<IRecord>(MockBehavior.Strict);

        //    record.Setup(s => s.Report("ModuleConfigure", false, false));
        //    record.Setup(s => s.Report("Startup", false, true));
        //    record.Setup(s => s.Report("Run", true, true));
        //    record.Setup(s => s.Report("GetRunSyncContext", false, false));
        //    record.Setup(s => s.Report("GetStartupSyncContext", false, false));

        //    var runContext = new TestSynchronizationContext();
        //    var startupContext = new TestSynchronizationContext();

        //    bootstrapper.ConfigureModulesAction = m => record.Object.Report("ModuleConfigure", runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.StartupActiveModuleAction = m => record.Object.Report("Startup", runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.OnStartupAction = () => record.Object.Report("Run", runContext.IsContextActive, startupContext.IsContextActive);
        //    bootstrapper.GetRunSyncContext = () => { record.Object.Report("GetRunSyncContext", runContext.IsContextActive, startupContext.IsContextActive); return runContext; };
        //    bootstrapper.GetStartupSyncContext = () => { record.Object.Report("GetStartupSyncContext", runContext.IsContextActive, startupContext.IsContextActive); return startupContext; };

        //    bootstrapper.Startup();

        //    record.VerifyAll();
        //}

        public interface IRecord
        {
            void Report(string id, bool inRunContext, bool inStartupContext);
        }

        public class BootstrapperMock : Bootstrapper
        {
            public BootstrapperMock()
            {
                ////this.CollectConfigurationAction = () => null;
                ////this.CreateExceptionHandlerAction = () => this.CreateExceptionHandlerMethod();
                ////this.CollectModulesAction = () => null;
                ////this.ConfigureContainerAction = container => this.ConfigureContainerMethod(container);
                ////this.ConfigureModulesAction = container => this.ConfigureModulesMethod(container);
                ////this.OnStartupAction = () => { };
                ////this.OnShutdownAction = () => this.CloseMethod();
                ////this.GetRunSyncContext = () => new TestSynchronizationContext();
                ////this.GetStartupSyncContext = () => new TestSynchronizationContext();
                ////this.StartupActiveModuleAction = manager => base.StartupActiveModules(manager);
                ////this.ShutdownActiveModuleAction = manager => base.ShutdownActiveModules(manager);
            }

            public Func<TestSynchronizationContext> GetStartupSyncContext { get; set; }

            public Func<TestSynchronizationContext> GetRunSyncContext { get; set; }


            public Func<IExceptionHandler> CreateExceptionHandlerAction { get; set; }

            public Action OnStartupAction { get; set; }

            public Action OnShutdownAction { get; set; }

            ////public IExceptionHandler CreateExceptionHandlerMethod()
            ////{
            ////    return base.CreateExceptionHandler();
            ////}

            ////public void CloseMethod()
            ////{
            ////    base.Close();
            ////}

            ////protected override void StartupActiveModules(IModuleManager manager)
            ////{
            ////    this.StartupActiveModuleAction(manager);
            ////}

            ////protected override void ShutdownActiveModules(IModuleManager manager)
            ////{
            ////    this.ShutdownActiveModuleAction(manager);
            ////}

            ////protected override SynchronizationContext RunSynchronizationContext
            ////{
            ////    get { return this.GetRunSyncContext(); }
            ////}

            ////protected override SynchronizationContext StartupSynchronizationContext
            ////{
            ////    get { return this.GetStartupSyncContext(); }
            ////}

            ////protected override IConfigurationSource CollectConfiguration()
            ////{
            ////    return this.CollectConfigurationAction();
            ////}

            ////protected override IExceptionHandler CreateExceptionHandler()
            ////{
            ////    return this.CreateExceptionHandlerAction();
            ////}

            ////protected override IModuleCatalog CollectModules()
            ////{
            ////    return this.CollectModulesAction();
            ////}

            ////protected override void ConfigureContainer(IUnityContainer container)
            ////{
            ////    this.ConfigureContainerAction(container);
            ////}

            ////protected override void ConfigureModules(IUnityContainer container)
            ////{
            ////    this.ConfigureModulesAction(container);
            ////}

            protected override void OnStartup()
            {
                this.OnStartupAction();
            }

            protected override void OnShutdown()
            {
                this.OnShutdownAction();
            }
        }

        public interface IComponent
        {
        }

        public class Component : IComponent
        {
        }
    }

    public class TestSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            this.IsContextActive = true;
            try
            {
                d(state);
            }
            finally
            {
                this.IsContextActive = false;
            }
        }

        public bool IsContextActive { get; private set; }
    }
}