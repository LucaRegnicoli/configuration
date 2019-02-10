using Infrastructure.Configuration.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// Extensions method for <see cref="IConfigurationRoot"/>
    /// </summary>
    internal static class InternalConfigurationRootExtensions
    {
        /// <summary>
        /// Gets the immediate children sub-sections of configuration root based on key.
        /// </summary>
        internal static IEnumerable<IConfigurationSection> GetChildrenImplementation(this IConfigurationRoot root, string path)
        {
            return root.Providers
                .Aggregate(Enumerable.Empty<string>(),
                    (seed, source) => source.GetChildKeys(seed, path))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(key => root.GetSection(path == null ? key : ConfigurationPath.Combine(path, key)));
        }
    }
}
