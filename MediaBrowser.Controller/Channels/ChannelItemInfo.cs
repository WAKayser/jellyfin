#nullable disable

#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Dto;
using MediaBrowser.Model.Entities;

namespace MediaBrowser.Controller.Channels
{
    public class ChannelItemInfo : IHasProviderIds
    {
        private Dictionary<string, string> _providerIds;
        public ChannelItemInfo()
        {
            MediaSources = new List<MediaSourceInfo>();
            TrailerTypes = new List<TrailerType>();
            Genres = new Collection<string>();
            Studios = new List<string>();
            People = new List<PersonInfo>();
            Tags = new List<string>();
            SetProviderId(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            Artists = new List<string>();
            AlbumArtists = new List<string>();
        }

        public string Name { get; set; }

        public string SeriesName { get; set; }

        public string Id { get; set; }

        public DateTime DateModified { get; set; }

        public ChannelItemType Type { get; set; }

        public string OfficialRating { get; set; }

        public string Overview { get; set; }

        public Collection<string> Genres { get; set; }

        public List<string> Studios { get; set; }

        public List<string> Tags { get; set; }

        public List<PersonInfo> People { get; set; }

        public float? CommunityRating { get; set; }

        public long? RunTimeTicks { get; set; }

        public string ImageUrl { get; set; }

        public string OriginalTitle { get; set; }

        public ChannelMediaType MediaType { get; set; }

        public ChannelFolderType FolderType { get; set; }

        public ChannelMediaContentType ContentType { get; set; }

        public ExtraType ExtraType { get; set; }

        public List<TrailerType> TrailerTypes { get; set; }

        public DateTime? PremiereDate { get; set; }

        public int? ProductionYear { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? IndexNumber { get; set; }

        public int? ParentIndexNumber { get; set; }

        public List<MediaSourceInfo> MediaSources { get; set; }

        public string HomePageUrl { get; set; }

        public List<string> Artists { get; set; }

        public List<string> AlbumArtists { get; set; }

        public bool IsLiveStream { get; set; }

        public string Etag { get; set; }

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
