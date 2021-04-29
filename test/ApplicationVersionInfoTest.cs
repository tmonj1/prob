using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Reflection;
using Xunit;
using Prob;
using FluentAssertions;

namespace ProbTest
{
    public class ApplicationVersionInfoTest
    {
        static readonly Assembly _systemAssembly;

        static ApplicationVersionInfoTest()
        {
            _systemAssembly = Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Debug.Assert(_systemAssembly != null);
        }

        [Fact]
        public void CanLoadAssemblyInfoSuccessfully()
        {
            // Arrange (nothing)

            // Act
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Assert
            assemblyInfo.Should().NotBeNull();
        }

        [Fact]
        public void NamePropertyIsValid()
        {
            // Arrange
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Act & Assert
            assemblyInfo.Name.Should().Equals("System");
        }

        [Fact]
        public void SemanticVersionPropertyIsValid()
        {
            // Arrange
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Act & Assert
            assemblyInfo.SemanticVersion.Should().Equals("4.0.0.0");
        }

        [Fact]
        public void VersionPropertyIsValid()
        {
            // Arrange
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Act & Assert
            assemblyInfo.Version.Should().Equals("4.0.0.0");
        }

        [Fact]
        public void InformationalVersionPropertyIsValid()
        {
            // Arrange
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Act & Assert
            assemblyInfo.InformationalVersion.Should().Equals("4.0.0.0");
        }

        [Fact]
        public void FileVersionPropertyIsValid()
        {
            // Arrange
            var assemblyInfo = _systemAssembly.GetApplicationVersionInfo();

            // Act & Assert
            assemblyInfo.FileVersion.Should().NotBeNullOrEmpty();
        }
    }
}