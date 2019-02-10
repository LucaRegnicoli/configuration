using Infrastructure.Configuration.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// Base helper class for implementing an <see cref="IConfigurationProvider"/>
    /// </summary>
    public abstract class ConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Initializes a new <see cref="IConfigurationProvider"/>
        /// </summary>
        protected ConfigurationProvider()
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// The configuration key value pairs for this provider.
        /// </summary>
        protected IDictionary<string, string> Data { get; set; }

        /// <summary>
        /// Attempts to find a value with the given key, returns true if one is found, false otherwise.
        /// </summary>
        public virtual bool TryGet(string key, out string value) => Data.TryGetValue(key, out value);

        /// <summary>
        /// Sets a value for a given key.
        /// </summary>
        public virtual void Set(string key, string value) => Data[key] = value;

        /// <summary>
        /// Loads the data for this provider.
        /// </summary>
        public virtual void Load() { }

        /// <summary>
        /// Returns the list of keys that this provider has.
        /// </summary>
        public virtual IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            var prefix = parentPath == null ? string.Empty : parentPath + ConfigurationPath.KeyDelimiter;

            return Data
                .Where(kv => kv.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(kv => Segment(kv.Key, prefix.Length))
                .Concat(earlierKeys);
        }

        /// <summary>
        /// Generates a string representing this provider name and relevant details.
        /// </summary>
        public override string ToString() => $"{GetType().Name}";

        private static string Segment(string key, int prefixLength)
        {
            var indexOf = key.IndexOf(ConfigurationPath.KeyDelimiter, prefixLength, StringComparison.OrdinalIgnoreCase);
            return indexOf < 0 ? key.Substring(prefixLength) : key.Substring(prefixLength, indexOf - prefixLength);
        }
    }
}