using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Configuration.FileExtensions
{
    /// <summary>
    /// Base class for file based <see cref="ConfigurationProvider"/>.
    /// </summary>
    public abstract class FileConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public FileConfigurationProvider(FileConfigurationSource source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// The source settings for this provider.
        /// </summary>
        public FileConfigurationSource Source { get; }

        /// <summary>
        /// Generates a string representing this provider name and relevant details.
        /// </summary>
        public override string ToString() => $"{GetType().Name} for '{Source.Path}' ({(Source.Optional ? "Optional" : "Required")})";

        /// <summary>
        /// Loads the contents of the file at <see cref="Path"/>.
        /// </summary>
        public override void Load()
        {
            var file = new FileInfo(Source.Path);
            if (file == null || !file.Exists)
            {
                if (Source.Optional)
                {
                    Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    var error = new StringBuilder($"The configuration file '{Source.Path}' was not found and is not optional.");
                    if (!string.IsNullOrEmpty(file?.FullName))
                    {
                        error.Append($" The physical path is '{file.FullName}'.");
                    }
                    HandleException(new FileNotFoundException(error.ToString()));
                }
            }
            else
            {
                var bufferSize = 1;
                using (var stream = new FileStream(
                            file.FullName,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.ReadWrite,
                            bufferSize,
                            FileOptions.Asynchronous | FileOptions.SequentialScan))
                {
                    try
                    {
                        Load(stream);
                    }
                    catch (Exception e)
                    {
                        HandleException(e);
                    }
                }
            }
        }

        /// <summary>
        /// Loads this provider's data from a stream.
        /// </summary>
        public abstract void Load(Stream stream);

        private void HandleException(Exception e)
        {
            bool ignoreException = false;
            if (Source.OnLoadException != null)
            {
                var exceptionContext = new FileLoadExceptionContext
                {
                    Provider = this,
                    Exception = e
                };
                Source.OnLoadException.Invoke(exceptionContext);
                ignoreException = exceptionContext.Ignore;
            }
            if (!ignoreException)
            {
                throw e;
            }
        }
    }
}
