# Introduction

The proposed configuration solution is based on key-value pairs established by configuration providers. 

Configuration providers read configuration data into key-value pairs from a variety of configuration sources:

* In-memory .NET objects
* INI file
* JSON file (not implemented in this version)
* XML file (not implemented in this version)

Configuration sources are read in the order that their configuration providers are specified.

The design allows a flexible extensibility of the model to support diffent scenarios and use cases:
One service could need just the in memory option, while another service might need the json AND xml options; the choice of which providers use (and in which order) is left to the API user.

The main design choice is the API **combines** the provided sources in one single key-value pairs object; for example:

```
// Create a new instance of the ConfigurationBuilder  
var config = new ConfigurationBuilder()
    .AddInMemoryCollection(arrayDictionary);
    .AddIniFile("data.ini", optional: false);
    .Build();

// Read the value
var value = config["Section:Key"];

```

Please see Infrastructure.Configuration.FunctionalTests project for a complete test scenario of the in-memory and ini providers.


# Data 

A generic example of a INI file:

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

* section0:key0
* section0:key1
* section1:subsection:key
* section2:subsection0:key
* section2:subsection1:key

In order to support JSON and XML configuration pattern, the API is capable of maintaining hierarchical configuration data by flattening the hierarchical data with the use of a delimiter in the configuration keys.

A generic example of JSON file:

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

* section0:key0
* section0:key1
* section1:key0
* section1:key1



#  Overrides

In order to support override, the choice is to use an approach similar to the object-oriented practice: a base configuration source that provides the base dataset and a specific 'derived' configuration file that contains the only overriden keys.

The 'derived' configuration file is added after the default file for example:

```
var config = new ConfigurationBuilder()
    .AddIniFile(IniDefaultFile)
    .AddIniFile(IniOverridenFile)
    .Build();
```

Please see Infrastructure.Configuration.FunctionalTests project for a complete test scenario of the override.

This approach is used in the Docker compose configuration (see https://docs.docker.com/compose/extends/) 

#  Projects description

#### Infrastructure.Configuration.FunctionalTests

#### Infrastructure.Configuration

#### Infrastructure.Configuration.Tests

#### Infrastructure.Configuration.Abstractions

#### Infrastructure.Configuration.FileExtensions

#### Infrastructure.Configuration.FunctionalTests

#### Infrastructure.Configuration.Ini

##### Infrastructure.Configuration.Ini.Tests

##### Infrastructure.Configuration.Json

##### Infrastructure.Configuration.Json.Tests






