# configuration
.NET APIs for configuration utilities.

```

// Arrange and Act 
var config = new ConfigurationBuilder()
    .AddIniFile(IniFile)
    .AddInMemoryCollection(MemoryConfigContent)
    .Build();

// Assert
Assert.Equal("IniValue1", config["IniKey1"]);
Assert.Equal("IniValue2", config["IniKey2:IniKey3"]);
Assert.Equal("IniValue3", config["IniKey2:IniKey4"]);
Assert.Equal("IniValue4", config["IniKey2:IniKey5:IniKey6"]);
Assert.Equal("IniValue5", config["CommonKey1:CommonKey2:IniKey7"]);

// Assert
Assert.Equal("MemValue1", config["MemKey1"]);
Assert.Equal("MemValue2", config["MemKey2:MemKey3"]);
Assert.Equal("MemValue3", config["MemKey2:MemKey4"]);
Assert.Equal("MemValue4", config["MemKey2:MemKey5:MemKey6"]);
Assert.Equal("MemValue5", config["CommonKey1:CommonKey2:MemKey7"]);
Assert.Equal("MemValue6", config["CommonKey1:CommonKey2:CommonKey3:CommonKey4"]);

```
