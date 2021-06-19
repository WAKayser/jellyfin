using System.Collections.Generic;

namespace MediaBrowser.Model.Entities
{
    /// <summary>
    /// Since BaseItem and DTOBaseItem both have ProviderIds, this interface helps avoid code repetition by using extension methods.
    /// </summary>
    public interface IHasProviderIds
    {
        /// <summary>
        /// Gets or sets the provider ids.
        /// </summary>
        /// <value>The provider ids.</value>
        /// <param name="providerIds">The id to be set.</param>
        public void SetProviderId(Dictionary<string, string> providerIds);

        /// <summary>
        /// Get the provider id.
        /// </summary>
        /// <returns>The provider id.</returns>
        public Dictionary<string, string> GetProviderId();
    }
}
