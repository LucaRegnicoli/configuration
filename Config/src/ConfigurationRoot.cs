using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configuration.Abstractions;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// The root node for a configuration.
    /// </summary>
    public class ConfigurationRoot : IConfigurationRoot, IDisposable
    {
        private readonly IList<IConfigurationProvider> _providers;

        /// <summary>
        /// Initializes a Configuration root with a list of providers.
        /// </summary>
        public ConfigurationRoot(IList<IConfigurationProvider> providers)
        {
            _providers = providers ?? throw new ArgumentNullException(nameof(providers));

            foreach (var p in providers)
            {
                p.Load();
            }
        }

        /// <summary>
        /// The <see cref="IConfigurationProvider"/>s for this configuration.
        /// </summary>
        public IEnumerable<IConfigurationProvider> Providers => _providers;

        /// <summary>
        /// Gets or sets the value corresponding to a configuration key.
        /// </summary>
        public string this[string key]
        {
            get
            {
                // Reverse the provider list to support the overrides
                foreach (var provider in _providers.Reverse())
                {
                    if (provider.TryGet(key, out var value))
                    {
                        return value;
                    }
                }

                return null;
            }
            set
            {
                if (!_providers.Any())
                {
                    throw new InvalidOperationException("Error_NoSources");
                }

                foreach (var provider in _providers)
                {
                    provider.Set(key, value);
                }
            }
        }

        /// <summary>
        /// Gets the immediate children sub-sections.
        /// </summary>
        public IEnumerable<IConfigurationSection> GetChildren() => this.GetChildrenImplementation(null);

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <remarks>
        ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
        ///     an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        public IConfigurationSection GetSection(string key) => new ConfigurationSection(this, key);

        /// <inheritdoc />
        public void Dispose()
        {
            // Dispose the providers
            foreach (var provider in _providers)
            {
                (provider as IDisposable)?.Dispose();
            }
        }
    }
}