using System.IO;
using Xunit;

namespace Infrastructure.Configuration.Ini.Tests
{
    public class ConfigurationExtensionsTest
    {
        [Fact]
        public void AddIniFile_WithRequiredPath_Should_Throws_When_FileDoesNotExist()
        {
            // Arrange
            var path = "file-does-not-exist.ini";

            // Act and Assert
            var ex = Assert.Throws<FileNotFoundException>(() => new ConfigurationBuilder().AddIniFile(path).Build());
            Assert.StartsWith($"The configuration file '{path}' was not found and is not optional. The physical path is '", ex.Message);
        }

        [Fact]
        public void AddIniFile_WithOptionalPath_Should_NotThrows_When_FileDoesNotExist()
        {
            // Arrange
            var path = "file-does-not-exist.ini";

            // Act and Assert
            new ConfigurationBuilder().AddIniFile(path, optional: true).Build();
        }
    }
}
