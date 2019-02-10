## Introduction

The proposed configuration solution is based on key-value pairs established by configuration providers. 

Configuration providers read configuration data into key-value pairs from a variety of configuration sources:

* In-memory .NET objects
* INI file
* JSON file (not implemented in this version)
* XML file (not implemented in this version)

Configuration sources are read in the order that their configuration providers are specified.

The design allows a flexible extensibility of the model to support diffent scenarios and use cases:
one service could need just the in-memory option, while another service might need the JSON and XML options; the choice of which providers use (and in which order) is left to the API user.

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

Please see Infrastructure.Configuration.FunctionalTests project for a complete test scenario of the in-memory and INI providers.

The configuration values, in the proposed solution, support only string value, but it's possible to extend the api in order to provide a Binder class that could deserialise the value into native object like boolean, integer or double, or convert the value into classes or list.

Note: The code coverage and the number of implemented tests is not high enough to be considered a production-ready api.

## Data 

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

Please see _Infrastructure.Configuration.FunctionalTests_ project for a complete test scenario of the override.

This approach is used in the Docker compose configuration (see https://docs.docker.com/compose/extends/) 

##  Projects description

#### Infrastructure.Configuration.FunctionalTests

It provides tests of the overall configuration solution: loading from different sources (In-memory and INI) and override (In-memory and INI).

#### Infrastructure.Configuration

It contains common classes like _ConfigurationBuilder_

#### Infrastructure.Configuration.Tests

It provides tests to common classes like _ConfigurationBuilder_

#### Infrastructure.Configuration.Abstractions

It contains interfaces that are implemented in the Infrastructure.Configuration project and referenced in the other projects.

#### Infrastructure.Configuration.FileExtensions

It contains the base classes for file-based provider (INI, JSON, XML).

#### Infrastructure.Configuration.Ini

It contains the extensions, provider and source class for Ini-based configuration.

Parsing logic (see _IniConfigurationProvider.Load_ for further details):

* Ignore blank lines
* Ignore comments (; # /)
* A section is enclosed in square bracket
* Key = value OR "value"
* Remove quotes
* Raise exception if the key is duplicated (but could be duplicated in other provider source)

#### Infrastructure.Configuration.Ini.Tests

It provides tests to ini-based configuration.

#### Infrastructure.Configuration.Json

This provider has not been implemented. It would provide the functionality to deserialise the key-value structures from a JSON file.

#### Infrastructure.Configuration.Json.Tests

This test suite has not been implemented. It would test the functionality to deserialise the key-value structures from a JSON file.

## Test result

The implemented tests complete successfully.

![Test result](https://github.com/LucaRegnicoli/configuration/blob/master/Test-Explore.png)





