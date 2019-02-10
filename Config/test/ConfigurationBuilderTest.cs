using System;
using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Configuration.Tests
{
    public class ConfigurationBuilderTest
    {
        [Fact]
        public void Build_FromDifferentProviders_Should_LoadAndCombine()
        {
            // Arrange
            var dictionary1 = new Dictionary<string, string>()
            {
                {"Mem1:KeyInMem1", "ValueInMem1"}
            };
            var dictionary2 = new Dictionary<string, string>()
            {
                {"Mem2:KeyInMem2", "ValueInMem2"}
            };
            var dictionary3 = new Dictionary<string, string>()
            {
                {"Mem3:KeyInMem3", "ValueInMem3"}
            };

            var memoryConfigurationSource1 = new MemoryConfigurationSource { InitialData = dictionary1 };
            var memoryConfigurationSource2 = new MemoryConfigurationSource { InitialData = dictionary2 };
            var memoryConfigurationSource3 = new MemoryConfigurationSource { InitialData = dictionary3 };
            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memoryConfigurationSource1);
            configurationBuilder.Add(memoryConfigurationSource2);
            configurationBuilder.Add(memoryConfigurationSource3);

            var config = configurationBuilder.Build();

            // Assert
            Assert.Contains(memoryConfigurationSource1, configurationBuilder.Sources);
            Assert.Contains(memoryConfigurationSource2, configurationBuilder.Sources);
            Assert.Contains(memoryConfigurationSource3, configurationBuilder.Sources);

            Assert.Equal("ValueInMem1", config["mem1:keyinmem1"]);
            Assert.Equal("ValueInMem2", config["Mem2:KeyInMem2"]);
            Assert.Equal("ValueInMem3", config["MEM3:KEYINMEM3"]);

            Assert.Equal("ValueInMem1", config["mem1:keyinmem1"]);
            Assert.Equal("ValueInMem2", config["Mem2:KeyInMem2"]);
            Assert.Equal("ValueInMem3", config["MEM3:KEYINMEM3"]);
            Assert.Null(config["NotExist"]);
        }

        [Fact]
        public void Build_FromDifferentProviders_Should_Overrides_When_KeyIsDuplicated()
        {
            // Arrange
            var dictionary1 = new Dictionary<string, string>()
            {
                {"Key1:Key2", "ValueInMem1"},
            };
            var dictionary2 = new Dictionary<string, string>()
            {
                {"Key1:Key2", "ValueInMem2"}
            };

            // Act
            var configurationRoot = 
                new ConfigurationBuilder()
                    .AddInMemoryCollection(dictionary1)
                    .AddInMemoryCollection(dictionary2)
                    .Build();

            // Assert
            Assert.Equal("ValueInMem2", configurationRoot["Key1:Key2"]);
        }

        [Fact]
        public void Build_WithoutProviders_Should_Throws()
        {
            // Arrange
            var expectedMessage = "Error_NoSources";

            // Act
            var configurationBuilder = new ConfigurationBuilder();
            var config = configurationBuilder.Build();
            var exception = Assert.Throws<InvalidOperationException>(() => config["Section"] = "Value");

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

    }
}
