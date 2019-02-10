using System.Collections.Generic;

namespace Infrastructure.Configuration.Abstractions
{
    /// <summary>
    /// Provides configuration key/values for an application.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Tries to get a configuration value for the specified key.
        /// </summary>
        bool TryGet(string key, out string value);

        /// <summary>
        /// Sets a configuration value for the specified key.
        /// </summary>
        void Set(string key, string value);

        /// <summary>
        /// Loads configuration values from the source represented by this <see cref="IConfigurationProvider"/>.
        /// </summary>
        void Load();

        /// <summary>
        /// Returns the immediate descendant configuration keys for a given parent path based on this
        /// <see cref="IConfigurationProvider"/>'s data and the set of keys returned by all the preceding
        /// <see cref="IConfigurationProvider"/>s.
        /// </summary>
        IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath);
    }
}