using MusicApp.Data.Entities;

namespace MusicApp.Data.ForViews
{
    public class AudioPlayerView
    {
        public PlayList CurrentPlayList { get; set; }
        public List<SongView> PlayListSongs { get; set; }
    }
}
