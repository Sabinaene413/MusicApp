using MusicApp.Data.Entities;
namespace MusicApp.Data
{
    public class PlayListDataForEdit
    {
        public PlayList PlayList { get; set; }
        public List<Song> Songs { get; set; }
    }
}
