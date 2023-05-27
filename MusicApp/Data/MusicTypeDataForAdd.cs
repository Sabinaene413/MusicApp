using MusicApp.Data.Entities;

namespace MusicApp.Data
{
    public class MusicTypeDataForAdd
    {
        public string Name { get; set;  }
        public List<Artist> Artists { get; set; }
        public List<PlayList> PlayLists { get; set; }
        public List<Song> Songs { get; set; }
        public List<int> SelectedArtists { get; set; }
        public List<int> SelectedPlayLists { get; set; }
        public List<int> SelectedSongs { get; set; }
    }
}
