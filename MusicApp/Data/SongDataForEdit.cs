using Microsoft.Identity.Client;
using MusicApp.Data.Entities;

namespace MusicApp.Data
{
    public class SongDataForEdit
    {
        public Song Song { get; set; }
        public List <Artist> Artists { get; set; }
    }
}
