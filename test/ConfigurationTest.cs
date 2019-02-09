using System.Collections.Generic;
using Xunit;

namespace Infrastructure.Configuration.FunctionalTests
{
    public class ConfigurationTest
    {
        private static readonly Dictionary<string, string> MemoryConfigContent = new Dictionary<string, string>
            {
                { "MemKey1", "MemValue1" },
                { "MemKey2:MemKey3", "MemValue2" },
                { "MemKey2:MemKey4", "MemValue3" },
                { "MemKey2:MemKey5:MemKey6", "MemValue4" },
                { "CommonKey1:CommonKey2:MemKey7", "MemValue5" },
                { "CommonKey1:CommonKey2:CommonKey3:CommonKey4", "MemValue6" }
            };


        [Fact]
        public void LoadAndCombineKeyValuePairsFromDifferentConfigurationProviders()
        {
            // Arrange and Act 
            var config = new ConfigurationBuilder()
                //.AddIniFile(_iniFile)
                .AddInMemoryCollection(MemoryConfigContent)
                .Build();

            //Assert.Equal("IniValue1", config["IniKey1"]);
            //Assert.Equal("IniValue2", config["IniKey2:IniKey3"]);
            //Assert.Equal("IniValue3", config["IniKey2:IniKey4"]);
            //Assert.Equal("IniValue4", config["IniKey2:IniKey5:IniKey6"]);
            //Assert.Equal("IniValue5", config["CommonKey1:CommonKey2:IniKey7"]);

            // Assert
            Assert.Equal("MemValue1", config["MemKey1"]);
            Assert.Equal("MemValue2", config["MemKey2:MemKey3"]);
            Assert.Equal("MemValue3", config["MemKey2:MemKey4"]);
            Assert.Equal("MemValue4", config["MemKey2:MemKey5:MemKey6"]);
            Assert.Equal("MemValue5", config["CommonKey1:CommonKey2:MemKey7"]);
            Assert.Equal("MemValue6", config["CommonKey1:CommonKey2:CommonKey3:CommonKey4"]);
        }
    }
}
