using System;
using System.Collections.Generic;

namespace Infrastructure.Configuration.Abstractions
{
    /// <summary>
    /// Represents a type used to build application configuration.
    /// </summary>
    public interface IConfigurationBuilder
    {
        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="IConfigurationBuilder"/>
        /// and the registered <see cref="IConfigurationSource"/>s.
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Gets the sources used to obtain configuration values
        /// </summary>
        IList<IConfigurationSource> Sources { get; }

        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        IConfigurationBuilder Add(IConfigurationSource source);

        /// <summary>
        /// Builds an <see cref="IConfiguration"/> with keys and values from the set of sources registered in
        /// <see cref="Sources"/>.
        /// </summary>
        IConfigurationRoot Build();
    }
}
