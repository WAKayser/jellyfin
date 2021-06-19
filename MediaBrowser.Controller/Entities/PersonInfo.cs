#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Entities
{
    /// <summary>
    /// This is a small Person stub that is attached to BaseItems.
    /// </summary>
    public sealed class PersonInfo : IHasProviderIds
    {
        private Dictionary<string, string> _providerIds;

        public PersonInfo()
        {
            SetProviderId(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
        }

        public Guid ItemId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the ascending sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int? SortOrder { get; set; }

        public string ImageUrl { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Name;
        }

        public bool IsType(string type)
        {
            return string.Equals(Type, type, StringComparison.OrdinalIgnoreCase)
                || string.Equals(Role, type, StringComparison.OrdinalIgnoreCase);
        }

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
