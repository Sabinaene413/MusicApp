namespace MusicApp.Data
{
    public class AddPlayListData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> SelectedSongs { get; set; }
    }
}
