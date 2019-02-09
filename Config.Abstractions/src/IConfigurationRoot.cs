using System.Collections.Generic;

namespace Infrastructure.Configuration.Abstractions
{
    /// <summary>
    /// Represents the root of an <see cref="IConfiguration"/> hierarchy.
    /// </summary>
    public interface IConfigurationRoot : IConfiguration
    {
        /// <summary>
        /// The <see cref="IConfigurationProvider"/>s for this configuration.
        /// </summary>
        IEnumerable<IConfigurationProvider> Providers { get; }
    }
}