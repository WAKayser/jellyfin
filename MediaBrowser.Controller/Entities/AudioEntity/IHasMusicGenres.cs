#nullable disable

#pragma warning disable CS1591

namespace MediaBrowser.Controller.Entities.AudioEntity
{
    public interface IHasMusicGenres
    {
        string[] Genres { get; }
    }
}
