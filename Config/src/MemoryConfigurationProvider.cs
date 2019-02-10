using Infrastructure.Configuration.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// In-memory implementation of <see cref="IConfigurationProvider"/>
    /// </summary>
    public class MemoryConfigurationProvider : ConfigurationProvider, IEnumerable<KeyValuePair<string, string>>
    {
        private readonly MemoryConfigurationSource _source;

        /// <summary>
        /// Initialize a new instance from the source.
        /// </summary>
        public MemoryConfigurationProvider(MemoryConfigurationSource source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            if (_source.InitialData != null)
            {
                foreach (var pair in _source.InitialData)
                {
                    Data.Add(pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// Add a new key and value pair.
        /// </summary>
        public void Add(string key, string value)
        {
            Data.Add(key, value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}