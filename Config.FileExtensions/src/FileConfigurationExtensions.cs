using Infrastructure.Configuration.Abstractions;
using System;

namespace Infrastructure.Configuration.FileExtensions
{
    /// <summary>
    /// Extension methods for <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public static class FileConfigurationExtensions
    {
        private static readonly string FileLoadExceptionHandlerKey = "FileLoadExceptionHandler";

        /// <summary>
        /// Gets the default <see cref="IFileProvider"/> to be used for file-based providers.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static Action<FileLoadExceptionContext> GetFileLoadExceptionHandler(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Properties.TryGetValue(FileLoadExceptionHandlerKey, out object handler))
            {
                return handler as Action<FileLoadExceptionContext>;
            }

            return null;
        }
    }
}
