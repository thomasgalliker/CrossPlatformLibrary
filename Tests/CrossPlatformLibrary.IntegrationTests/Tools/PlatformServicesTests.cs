﻿using System;
using System.Linq;

using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tools;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.IntegrationTests.Tools
{
    [Trait("Category", "IntegrationTests")]
    public class PlatformServicesTests : IDisposable
    {
        private readonly Bootstrapper bootstrapper;

        public PlatformServicesTests()
        {
            this.bootstrapper = new Bootstrapper();
            this.bootstrapper.Startup();
        }

        [Fact]
        public void ShouldGetInstanceOfPlatformServices()
        {
            // Act
            var platformServices = SimpleIoc.Default.GetInstance<IPlatformServices>();

            // Assert
            platformServices.Should().NotBeNull();
        }

        [Fact]
        public void ShouldGetAssemblies()
        {
            // Arrange
            var platformServices = SimpleIoc.Default.GetInstance<IPlatformServices>();

            // Act
            var assemblies = platformServices.GetAssemblies();

            // Assert
            assemblies.Count().Should().BeGreaterThan(0);
            assemblies.Should().Contain(assembly => assembly.FullName.Contains("CrossPlatformLibrary"));
        }

        [Fact]
        public void ShouldLoadReferencedAssemblies()
        {
            // Arrange
            var platformServices = SimpleIoc.Default.GetInstance<IPlatformServices>();
            var assembliesBeforeLoad = platformServices.GetAssemblies();

            // Act
            platformServices.LoadReferencedAssemblies();

            // Assert
            var assembliesAfterLoad = platformServices.GetAssemblies();
            assembliesBeforeLoad.Should().HaveCount(assembliesAfterLoad.Count(),
                "assembliesBeforeLoad and assembliesAfterLoad should have the same assemblies " + 
                "because LoadReferencedAssemblies is already done at startup time.");
        }

        public void Dispose()
        {
            this.bootstrapper.Shutdown();
        }
    }
}
