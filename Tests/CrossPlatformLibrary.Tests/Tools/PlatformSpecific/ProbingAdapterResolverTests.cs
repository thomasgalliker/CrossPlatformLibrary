using System;
using System.Reflection;

using CrossPlatformLibrary.Tests.Stubs;
using CrossPlatformLibrary.Tools;
using CrossPlatformLibrary.Tools.PlatformSpecific;
using CrossPlatformLibrary.Tools.PlatformSpecific.Exceptions;

using FluentAssertions;

using Moq;

using Xunit;

namespace CrossPlatformLibrary.Tests.Tools.PlatformSpecific
{
    public class ProbingAdapterResolverTests
    {
        [Fact]
        public void ShouldResolvePlatformSpecificClassTypeForInterface()
        {
            // Arrange
            var testRegistrationConvention = new TestRegistrationConvention();
            var probingAdapterResolver = new ProbingAdapterResolver(testRegistrationConvention);
            var interfaceToResolve = typeof(IPlatformServices);

            // Act
            var classType = probingAdapterResolver.ResolveClassType(interfaceToResolve);

            // Assert
            classType.Should().Be<PlatformServicesStub>();
        }

        [Fact]
        public void ShouldOverrideDefaultRegistrationConvention()
        {
            // Arrange
            var registrationConventionMock = new Mock<IRegistrationConvention>();
            var probingAdapterResolver = new ProbingAdapterResolver(registrationConventionMock.Object);
            var interfaceToResolve = typeof(IPlatformServices);

            // Act
            probingAdapterResolver.RegistrationConvention = new TestRegistrationConvention();

            // Assert
            var classType = probingAdapterResolver.ResolveClassType(interfaceToResolve);
            classType.Should().Be<PlatformServicesStub>();
        }

        [Fact]
        public void ShouldNotThrowAnyExceptionIfFlagIsSet()
        {
            // Arrange
            const bool ThrowIfNotFound = false;
            var probingAdapterResolver = new ProbingAdapterResolver(); // Default ctor uses DefaultRegistrationConvention which doesnt work with unit tests
            var interfaceToResolve = typeof(IPlatformServices);

            // Act
            var classType = probingAdapterResolver.ResolveClassType(interfaceToResolve, ThrowIfNotFound);

            // Assert
            classType.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowPlatformNotSupportedExceptionIfUnableToProbeForPlatformSpecificAssembly()
        {
            // Arrange
            var registrationConventionMock = new Mock<IRegistrationConvention>();
            var probingAdapterResolver = new ProbingAdapterResolver(registrationConventionMock.Object);
            var interfaceToResolve = typeof(IPlatformServices);

            // Act
            Action resolveAction = () => probingAdapterResolver.ResolveClassType(interfaceToResolve);

            // Assert
            Assert.Throws<PlatformSpecificAssemblyNotFoundException>(resolveAction);
        }

        [Fact]
        public void ShouldThrowPlatformNotSupportedExceptionIfUnableToFindTargetClassInAssembly()
        {
            // Arrange
            var registrationConventionMock = new Mock<IRegistrationConvention>();
            registrationConventionMock.Setup(registrationConvention => registrationConvention.PlatformNamingConvention(It.IsAny<AssemblyName>()))
                .Returns((AssemblyName assemblyName) => assemblyName.Name);
            registrationConventionMock.Setup(registrationConvention => registrationConvention.InterfaceToClassNamingConvention(It.IsAny<Type>()))
                .Returns((Type t) => "TypeWhichDoesNotExist");

            var probingAdapterResolver = new ProbingAdapterResolver(registrationConventionMock.Object);
            var interfaceToResolve = typeof(IServiceWithNoImplementation);

            // Act
            Action resolveAction = () => probingAdapterResolver.ResolveClassType(interfaceToResolve);

            // Assert
            Assert.Throws<PlatformSpecificTypeNotFoundException>(resolveAction);
        }
    }
}