using Infrastructure.Configuration.Abstractions;
using System;

namespace Infrastructure.Configuration.Ini
{
    /// <summary>
    /// Extension methods for adding <see cref="IniConfigurationProvider"/>.
    /// </summary>
    public static class IniConfigurationExtensions
    {
        /// <summary>
        /// Adds the INI configuration provider at <paramref name="path"/> to <paramref name="builder"/>.
        /// </summary>
        public static IConfigurationBuilder AddIniFile(this IConfigurationBuilder builder, string path)
        {
            return AddIniFile(builder, path: path, optional: false);
        }

        /// <summary>
        /// Adds a INI configuration source to <paramref name="builder"/>.
        /// </summary>
        public static IConfigurationBuilder AddIniFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Error_InvalidFilePath", nameof(path));
            }

            return builder.AddIniFile(s =>
            {
                s.Path = path;
                s.Optional = optional;
            });
        }

        /// <summary>
        /// Adds a INI configuration source to <paramref name="builder"/>.
        /// </summary>
        public static IConfigurationBuilder AddIniFile(this IConfigurationBuilder builder, Action<IniConfigurationSource> configureSource)
        {
            var source = new IniConfigurationSource();
            configureSource?.Invoke(source);
            return builder.Add(source);
        }
    }
}
