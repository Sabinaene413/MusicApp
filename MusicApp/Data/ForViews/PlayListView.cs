using MusicApp.Data.Entities;

namespace MusicApp.Data.ForViews
{
    public class PlayListView
    {
        public PlayList PlayList { get; set; }
        public List<SongView> Songs { get; set; }

    }
}
