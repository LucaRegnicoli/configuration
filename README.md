# Configuration

The proposed configuration solution is based on key-value pairs established by configuration providers. 
Configuration providers read configuration data into key-value pairs from a variety of configuration sources:

* In-memory .NET objects
* INI file
* JSON file (not implemented in this version)
* XML file (not implemented in this version)

Configuration sources are read in the order that their configuration providers are specified.

```
// Create a new instance of the ConfigurationBuilder  
var config = new ConfigurationBuilder()
    .AddInMemoryCollection(arrayDictionary);
    .AddIniFile("data.ini", optional: false);
    .Build();

// Read the value
var value = config["CommonKey1:CommonKey2:CommonKey3:CommonKey4"];

```

#  Projects description

#### Infrastructure.Configuration.FunctionalTests

#### Infrastructure.Configuration

#### Infrastructure.Configuration.Tests

#### Infrastructure.Configuration.Abstractions

#### Infrastructure.Configuration.FileExtensions

#### Infrastructure.Configuration.FunctionalTests

#### Infrastructure.Configuration.Ini

## Infrastructure.Configuration.Ini.Tests

## Infrastructure.Configuration.Json

## Infrastructure.Configuration.Json.Tests






