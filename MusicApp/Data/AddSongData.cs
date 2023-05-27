namespace MusicApp.Data
{
    public class AddSongData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> SelectedArtists { get; set; }
    }
}
