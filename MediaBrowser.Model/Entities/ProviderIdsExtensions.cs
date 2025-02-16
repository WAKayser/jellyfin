using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MediaBrowser.Model.Entities
{
    /// <summary>
    /// Class ProviderIdsExtensions.
    /// </summary>
    public static class ProviderIdsExtensions
    {
        /// <summary>
        /// Checks if this instance has an id for the given provider.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The of the provider name.</param>
        /// <returns><c>true</c> if a provider id with the given name was found; otherwise <c>false</c>.</returns>
        public static bool HasProviderId(this IHasProviderIds instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.TryGetProviderId(name, out _);
        }

        /// <summary>
        /// Checks if this instance has an id for the given provider.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="provider">The provider.</param>
        /// <returns><c>true</c> if a provider id with the given name was found; otherwise <c>false</c>.</returns>
        public static bool HasProviderId(this IHasProviderIds instance, MetadataProvider provider)
        {
            return instance.HasProviderId(provider.ToString());
        }

        /// <summary>
        /// Gets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="id">The provider id.</param>
        /// <returns><c>true</c> if a provider id with the given name was found; otherwise <c>false</c>.</returns>
        public static bool TryGetProviderId(this IHasProviderIds instance, string name, [NotNullWhen(true)] out string? id)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (instance.GetProviderId() == null)
            {
                id = null;
                return false;
            }

            var foundProviderId = instance.GetProviderId().TryGetValue(name, out id);
            // This occurs when searching with Identify (and possibly in other places)
            if (string.IsNullOrEmpty(id))
            {
                id = null;
                foundProviderId = false;
            }

            return foundProviderId;
        }

        /// <summary>
        /// Gets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="id">The provider id.</param>
        /// <returns><c>true</c> if a provider id with the given name was found; otherwise <c>false</c>.</returns>
        public static bool TryGetProviderId(this IHasProviderIds instance, MetadataProvider provider, [NotNullWhen(true)] out string? id)
        {
            return instance.TryGetProviderId(provider.ToString(), out id);
        }

        /// <summary>
        /// Gets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public static string? GetProviderId(this IHasProviderIds instance, string name)
        {
            instance.TryGetProviderId(name, out string? id);
            return id;
        }

        /// <summary>
        /// Gets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>System.String.</returns>
        public static string? GetProviderId(this IHasProviderIds instance, MetadataProvider provider)
        {
            return instance.GetProviderId(provider.ToString());
        }

        /// <summary>
        /// Sets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void SetProviderId(this IHasProviderIds instance, string name, string value)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var copy = instance.GetProviderId();

            // If it's null remove the key from the dictionary
            if (string.IsNullOrEmpty(value))
            {
                copy.Remove(name);
            }
            else
            {
                // Ensure it exists
                copy ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                copy[name] = value;
            }

            instance.SetProviderId(copy);
        }

        /// <summary>
        /// Sets a provider id.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="value">The value.</param>
        public static void SetProviderId(this IHasProviderIds instance, MetadataProvider provider, string value)
        {
            instance.SetProviderId(provider.ToString(), value);
        }
    }
}
