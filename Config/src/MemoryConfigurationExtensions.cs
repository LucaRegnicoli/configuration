using Infrastructure.Configuration.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// IConfigurationBuilder extension methods for the MemoryConfigurationProvider.
    /// </summary>
    public static class MemoryConfigurationExtensions
    {
        /// <summary>
        /// Adds the memory configuration provider to <paramref name="configurationBuilder"/>.
        /// </summary>
        public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new MemoryConfigurationSource());
            return configurationBuilder;
        }

        /// <summary>
        /// Adds the memory configuration provider to <paramref name="configurationBuilder"/>.
        /// </summary>
        public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder, IEnumerable<KeyValuePair<string, string>> initialData)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new MemoryConfigurationSource { InitialData = initialData });
            return configurationBuilder;
        }
    }
}
