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
            Tracer.Initialize();
        }

        public void Dispose()
        {
            Tracer.Initialize();
        }

        [Fact]
        public void ShouldReturnDefaultFactoryIfNoFactoryIsSet()
        {
            // Act
            var factory = Tracer.Factory;

            // Assert
            Assert.IsType<EmptyTracerFactory>(factory);
        }

        [Fact]
        public void ShouldSetFactory()
        {
            // Arrange
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();

            // Act
            Tracer.SetFactory(factoryMock.Object);

            // Assert
            Assert.Same(factoryMock.Object, Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void ShouldReturnDefaultFactoryIfFactoryIsSetNull()
        {
            // Arrange
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();

            Tracer.SetFactory(factoryMock.Object);
            Assert.Same(factoryMock.Object, Tracer.Factory);

            // Act
            Tracer.SetFactory(null);

            // Assert
            Assert.IsType<EmptyTracerFactory>(Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void ShouldOverrideDefaultFactory()
        {
            // Arrange
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Tracer.SetFactory(null);

            // Act
            Tracer.SetDefaultFactory(factoryMock.Object);

            // Assert
            Assert.Same(factoryMock.Object, Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithNameCallsCreateOnFactory()
        {
            // Arrange
            MockRepository mocks = new MockRepository(MockBehavior.Strict);
            Mock<ITracerFactory> factoryMock = mocks.Create<ITracerFactory>();
            Mock<ITracer> tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create("Name")).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);

            // Act
            Tracer.Create("Name");

            // Assert
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