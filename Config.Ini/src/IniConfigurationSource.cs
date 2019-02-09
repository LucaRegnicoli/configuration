using System;
using Infrastructure.Configuration.Abstractions;
using Infrastructure.Configuration.FileExtensions;

namespace Infrastructure.Configuration.Ini
{
    /// <summary>
    /// Represents an INI file as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class IniConfigurationSource : FileConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IniConfigurationProvider"/> for this source.
        /// </summary>
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new IniConfigurationProvider(this);
        }
    }
}
