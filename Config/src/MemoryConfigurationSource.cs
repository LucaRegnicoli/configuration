using Infrastructure.Configuration.Abstractions;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// Represents in-memory data as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class MemoryConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// The initial key value configuration pairs.
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> InitialData { get; set; }

        /// <summary>
        /// Builds the <see cref="MemoryConfigurationProvider"/> for this source.
        /// </summary>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MemoryConfigurationProvider(this);
        }
    }
}