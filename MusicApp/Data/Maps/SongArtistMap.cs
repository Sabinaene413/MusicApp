using System.ComponentModel.DataAnnotations;

namespace MusicApp.Data.Maps
{
    public class SongArtistMap
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SongId { get; set; }
        public int ArtistId { get; set; }

    }
}
