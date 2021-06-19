#pragma warning disable CS1591


namespace MediaBrowser.Providers.Plugins.Omdb
{
    /// <summary>
    /// OMDB provider.
    /// </summary>
    public partial class OmdbProvider
    {
        public class OmdbRating
        {
            public string Source { get; set; }

            public string Value { get; set; }
        }
    }
}
