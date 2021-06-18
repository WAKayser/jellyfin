#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MediaBrowser.Controller.Providers
{
    public class AlbumInfo : ItemLookupInfo
    {
        private Dictionary<string, string>? _artistProviderIds;

        private Collection<SongInfo>? _songInfos;

        public AlbumInfo()
        {
            SetArtistProviderIds(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            SetSongInfos(new Collection<SongInfo>());
            AlbumArtists = Array.Empty<string>();
        }

        /// <summary>
        /// Gets or sets the album artist.
        /// </summary>
        /// <value>The album artist.</value>
        public IReadOnlyList<string> AlbumArtists { get; set; }

        /// <summary>
        /// Gets the artist provider ids.
        /// </summary>
        /// <returns>The artist provider ids.</returns>
        public Dictionary<string, string> GetArtistProviderIds()
        {
            return _artistProviderIds!;
        }

        /// <summary>
        /// Gets the artist provider ids.
        /// </summary>
        /// <param name="value">The artist provider ids.</param>
        public void SetArtistProviderIds(Dictionary<string, string> value)
        {
            _artistProviderIds = value;
        }

        public Collection<SongInfo> GetSongInfos()
        {
            return _songInfos!;
        }

        public void SetSongInfos(Collection<SongInfo> value)
        {
            _songInfos = value;
        }
    }
}
