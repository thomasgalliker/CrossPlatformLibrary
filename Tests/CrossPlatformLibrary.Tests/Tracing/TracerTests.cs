using System;

using CrossPlatformLibrary.Tracing;

using Moq;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tracing
{
    [Collection("Tracing")]
    public class TracerTests : IDisposable
    {
        public TracerTests()
        {
            Tracer.SetFactory(null);
        }

        public void Dispose()
        {
            Tracer.SetFactory(null);
        }


        [Fact]
        public void IfNoFactoryIsSetTheDefaultFactoryIsUsed()
        {
            Assert.IsType<EmptyTracerFactory>(Tracer.Factory);
        }

        [Fact]
        public void SetFactorySetsCorrectFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();

            Tracer.SetFactory(factoryMock.Object);
            Assert.Same(factoryMock.Object, Tracer.Factory);

            mocks.VerifyAll();
        }

        [Fact]
        public void SetFactoryWithNullFactorySetsDefaultFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();

            Tracer.SetFactory(factoryMock.Object);
            Assert.Same(factoryMock.Object, Tracer.Factory);

            Tracer.SetFactory(null);
            Assert.IsType<EmptyTracerFactory>(Tracer.Factory);

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithNameCallsCreateOnFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create("Name")).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            Tracer.Create("Name");

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithNameReturnsTracerReturnedByFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create("Name")).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create("Name");
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithTypeCallsCreateOnFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create(typeof(Type))).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            Tracer.Create(typeof(Type));

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithTypeReturnsTracerReturnedByFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create(typeof(Type))).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create(typeof(Type));
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithGenericTypeCallsCreateOnFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create<Type>()).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            Tracer.Create<Type>();

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithGenericTypeReturnsTracerReturnedByFactory()
        {
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create<Type>()).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create<Type>();
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }
    }
}