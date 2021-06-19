#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Subtitles
{
    public class SubtitleSearchRequest : IHasProviderIds
    {
        private Dictionary<string, string> _providerIds;

        public string Language { get; set; }

        public string TwoLetterISOLanguageName { get; set; }

        public VideoContentType ContentType { get; set; }

        public string MediaPath { get; set; }

        public string SeriesName { get; set; }

        public string Name { get; set; }

        public int? IndexNumber { get; set; }

        public int? IndexNumberEnd { get; set; }

        public int? ParentIndexNumber { get; set; }

        public int? ProductionYear { get; set; }

        public long? RuntimeTicks { get; set; }

        public bool IsPerfectMatch { get; set; }

        public bool SearchAllProviders { get; set; }

        public string[] DisabledSubtitleFetchers { get; set; }

        public string[] SubtitleFetcherOrder { get; set; }

        public SubtitleSearchRequest()
        {
            SearchAllProviders = true;
            SetProviderId(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));

            DisabledSubtitleFetchers = Array.Empty<string>();
            SubtitleFetcherOrder = Array.Empty<string>();
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
    }
}
