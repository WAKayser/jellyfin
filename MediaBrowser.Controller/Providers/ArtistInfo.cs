#pragma warning disable CS1591

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MediaBrowser.Controller.Providers
{
    public class ArtistInfo : ItemLookupInfo
    {
        private Collection<SongInfo>? _songInfos;

        public ArtistInfo()
        {
            SetSongInfos(new Collection<SongInfo>());
        }

        public Collection<SongInfo> SongInfos => _songInfos!;

        public void SetSongInfos(Collection<SongInfo> value)
        {
            _songInfos = value;
        }
    }
}
