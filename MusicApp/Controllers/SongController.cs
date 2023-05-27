using Microsoft.AspNetCore.Mvc;
using MusicApp.Data.Entities;
using MusicApp.Data.ForViews;
using MusicApp.Data.Maps;
using MusicApp.Data;

namespace MusicApp.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditSong(int id)
        {
            var songData = new SongDataForEdit();
            using (var context = new MyDBContext())
            {
                songData.Song = context.Song.FirstOrDefault(a => a.Id == id);
                songData.Artists = context.Artist.ToList();
            }
            return View(songData);
        }

        [HttpPost]
        public IActionResult UpdateSong(AddSongData songData)
        {
            using (var context = new MyDBContext())
            {
                var existingSong = context.Song.FirstOrDefault(a => a.Id == songData.Id);
                existingSong.Name = songData.Name;

                var mappingsToDelete = context.SongArtistMap
                    .Where(map => map.SongId == existingSong.Id)
                    .ToList();
                context.SongArtistMap.RemoveRange(mappingsToDelete);
                context.SaveChanges();
            }
            //sterge maparile vechi si le introduce pe cele noi   || artisti
            List<SongArtistMap> songArtists = new List<SongArtistMap>();
            foreach (var artist in songData.SelectedArtists)
            {
                var songArtist = new SongArtistMap()
                {
                    SongId = songData.Id,
                    ArtistId = artist
                };
                songArtists.Add(songArtist);

            }

            using (var context = new MyDBContext())
            {
                context.SongArtistMap.AddRange(songArtists);
                context.SaveChanges();
            }

            return RedirectToAction("ViewSongs");
        }

        public IActionResult DeleteSong(int id)
        {
            using (var context = new MyDBContext())
            {
                var song = context.Song.FirstOrDefault(a => a.Id == id);

                context.SongArtistMap.RemoveRange(context.SongArtistMap.Where(x => x.SongId == song.Id));
                context.SaveChanges();
                context.Song.Remove(song);
                context.SaveChanges();
            }

            return RedirectToAction("ViewSongs");
        }

        public IActionResult AddSong()
        {
            List<Artist> artists = new List<Artist>();
            using (var context = new MyDBContext())
            {
                artists = context.Artist.ToList();
            }
            return View(artists);
        }

        [HttpPost]
        public IActionResult AddSong(AddSongData songAddData)
        {
            Song song = new Song()
            {
                Name = songAddData.Name,
            };

            using (var context = new MyDBContext())
            {
                context.Song.Add(song);
                context.SaveChanges();
            }
            List<SongArtistMap> songArtists = new List<SongArtistMap>();
            foreach (var artist in songAddData.SelectedArtists)
            {
                var songArtist = new SongArtistMap()
                {
                    SongId = song.Id,
                    ArtistId = artist
                };
                songArtists.Add(songArtist);

            }

            using (var context = new MyDBContext())
            {
                context.SongArtistMap.AddRange(songArtists);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewSongs()
        {
            var songArtistMap = new List<SongArtistMap>();
            var songs = new List<Song>();
            var artists = new List<Artist>();
            using (var context = new MyDBContext())
            {
                songArtistMap = context.SongArtistMap.ToList();
                songs = context.Song.ToList();
                artists = context.Artist.ToList();
            }
            var songData = new List<SongView>();

            foreach (var song in songs)
            {
                var associatedArtists = songArtistMap
                    .Where(map => map.SongId == song.Id)
                    .Join(artists, map => map.ArtistId, artist => artist.Id, (map, artist) => artist.Name)
                    .ToList();

                var viewModel = new SongView
                {
                    Id = song.Id,
                    Name = song.Name,
                    ArtistNames = associatedArtists
                };

                songData.Add(viewModel);
            }

            return View(songData);
        }
    }
}
