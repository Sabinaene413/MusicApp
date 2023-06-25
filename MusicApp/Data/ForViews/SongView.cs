namespace MusicApp.Data.ForViews
{
    public class SongView
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string FilePath { get; set; }
        public List<string> ArtistNames { get; set; }
    }
}
