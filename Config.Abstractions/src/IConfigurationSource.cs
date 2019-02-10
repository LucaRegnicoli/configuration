namespace Infrastructure.Configuration.Abstractions
{
    /// <summary>
    /// Represents a source of configuration key/values for an application.
    /// </summary>
    public interface IConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        IConfigurationProvider Build(IConfigurationBuilder builder);
    }
}