# Introduction

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
var value = config["Section:Key"];

```

Please see Infrastructure.Configuration.FunctionalTests project for a complete test scenario.

# Data 

In order to support JSON and XML configuration pattern, the API is capable of maintaining hierarchical configuration data by flattening the hierarchical data with the use of a delimiter in the configuration keys, so for example a JSON file:

```
{
  "section0": {
    "key0": "value",
    "key1": "value"
  },
  "section1": {
    "key0": "value",
    "key1": "value"
  }
}
```

The sections and keys are flattened with the use of a colon (:) to maintain the original structure:

section0:key0
section0:key1
section1:key0
section1:key1

A generic example of a INI file

```
[section0]
key0=value
key1=value

[section1]
subsection:key=value

[section2:subsection0]
key=value

[section2:subsection1]
key=value

```

The previous configuration file loads the following keys with value:

section0:key0
section0:key1
section1:subsection:key
section2:subsection0:key
section2:subsection1:key


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






