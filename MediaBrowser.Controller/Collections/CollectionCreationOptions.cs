#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Collections
{
    public class CollectionCreationOptions : IHasProviderIds
    {
        private Dictionary<string, string> _providerIds;

        public CollectionCreationOptions()
        {
            SetProviderId(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            ItemIdList = Array.Empty<string>();
            UserIds = Array.Empty<Guid>();
        }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsLocked { get; set; }

        public IReadOnlyList<string> ItemIdList { get; set; }

        public IReadOnlyList<Guid> UserIds { get; set; }

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
    }
}
