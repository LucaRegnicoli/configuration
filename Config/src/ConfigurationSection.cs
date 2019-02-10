using Infrastructure.Configuration.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    /// <summary>
    /// Represents a section of application configuration values.
    /// </summary>
    public class ConfigurationSection : IConfigurationSection
    {
        private readonly IConfigurationRoot _root;
        private readonly string _path;
        private string _key;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ConfigurationSection(IConfigurationRoot root, string path)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        /// <summary>
        /// Gets the full path to this section from the <see cref="IConfigurationRoot"/>.
        /// </summary>
        public string Path => _path;

        /// <summary>
        /// Gets the key this section occupies in its parent.
        /// </summary>
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = ConfigurationPath.GetSectionKey(_path);
                }
                return _key;
            }
        }

        /// <summary>
        /// Gets or sets the section value.
        /// </summary>
        public string Value
        {
            get
            {
                return _root[Path];
            }
            set
            {
                _root[Path] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value corresponding to a configuration key.
        /// </summary>
        public string this[string key]
        {
            get
            {
                return _root[ConfigurationPath.Combine(Path, key)];
            }

            set
            {
                _root[ConfigurationPath.Combine(Path, key)] = value;
            }
        }

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <remarks>
        ///     This method will never return <c>null</c>. If no matching sub-section is found with the specified key,
        ///     an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        public IConfigurationSection GetSection(string key) => _root.GetSection(ConfigurationPath.Combine(Path, key));

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        public IEnumerable<IConfigurationSection> GetChildren() => _root.GetChildrenImplementation(Path);
    }
}