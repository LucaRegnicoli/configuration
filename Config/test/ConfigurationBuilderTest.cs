using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Configuration.Tests
{
    public class ConfigurationBuilderTest
    {
        [Fact]
        public void LoadAndCombineKeyValuePairsFromDifferentConfigurationProviders()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Mem1:KeyInMem1", "ValueInMem1"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Mem2:KeyInMem2", "ValueInMem2"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"Mem3:KeyInMem3", "ValueInMem3"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            var memVal1 = config["mem1:keyinmem1"];
            var memVal2 = config["Mem2:KeyInMem2"];
            var memVal3 = config["MEM3:KEYINMEM3"];

            // Assert
            Assert.Contains(memConfigSrc1, configurationBuilder.Sources);
            Assert.Contains(memConfigSrc2, configurationBuilder.Sources);
            Assert.Contains(memConfigSrc3, configurationBuilder.Sources);

            Assert.Equal("ValueInMem1", memVal1);
            Assert.Equal("ValueInMem2", memVal2);
            Assert.Equal("ValueInMem3", memVal3);

            Assert.Equal("ValueInMem1", config["mem1:keyinmem1"]);
            Assert.Equal("ValueInMem2", config["Mem2:KeyInMem2"]);
            Assert.Equal("ValueInMem3", config["MEM3:KEYINMEM3"]);
            Assert.Null(config["NotExist"]);
        }

        [Fact]
        public void NewConfigurationProviderOverridesOldOneWhenKeyIsDuplicated()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Key1:Key2", "ValueInMem1"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Key1:Key2", "ValueInMem2"}
            };

            // Act
            var configurationRoot = 
                new ConfigurationBuilder()
                    .AddInMemoryCollection(dic1)
                    .AddInMemoryCollection(dic2)
                    .Build();

            // Assert
            Assert.Equal("ValueInMem2", configurationRoot["Key1:Key2"]);
        }
    }
}
