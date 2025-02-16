#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Providers
{
    public class ItemLookupInfo : IHasProviderIds
    {
        private Dictionary<string, string> _providerIds;

        public ItemLookupInfo()
        {
            IsAutomated = true;
            SetProviderId(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the original title
        /// </summary>
        /// <value>The original title of the item.</value>
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the metadata language.
        /// </summary>
        /// <value>The metadata language.</value>
        public string MetadataLanguage { get; set; }

        /// <summary>
        /// Gets or sets the metadata country code.
        /// </summary>
        /// <value>The metadata country code.</value>
        public string MetadataCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int? Year { get; set; }

        public int? IndexNumber { get; set; }

        public int? ParentIndexNumber { get; set; }

        public DateTime? PremiereDate { get; set; }

        public bool IsAutomated { get; set; }

        /// <summary>
        /// Gets or sets the provider ids.
        /// </summary>
        /// <value>The provider ids.</value>
        /// <param name="providerIds">Set the ID.</param>
        public void SetProviderId(Dictionary<string, string> providerIds)
        {
            _providerIds = providerIds;
        }

        public Dictionary<string, string> GetProviderId() => _providerIds;

        public void SetProviderIdValue(string name, string value)
        {
            // If it's null remove the key from the dictionary
            if (string.IsNullOrEmpty(value))
            {
                _providerIds!.Remove(name);
            }
            else
            {
                // Ensure it exists
                _providerIds ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                _providerIds[name] = value;
            }
        }
    }
}
