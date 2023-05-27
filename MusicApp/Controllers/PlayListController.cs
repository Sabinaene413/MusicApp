using Microsoft.AspNetCore.Mvc;
using MusicApp.Data.Entities;
using MusicApp.Data.Maps;
using MusicApp.Data;
using MusicApp.Data.ForViews;

namespace MusicApp.Controllers
{
    public class PlayListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewPlayLists()
        {
            var playLists = new List<PlayList>();
            using (var context = new MyDBContext())
            {
                playLists = context.PlayList.ToList();
            }
            return View(playLists);
        }

        public IActionResult AddPlayList()
        {
            List<Song> songs = new List<Song>();
            using (var context = new MyDBContext())
            {
                songs = context.Song.ToList();
            }
            return View(songs);
        }

        [HttpPost]
        public IActionResult AddPlayList(AddPlayListData playListAddData)
        {
            PlayList playList = new PlayList()
            {
                Name = playListAddData.Name,
            };

            using (var context = new MyDBContext())
            {
                context.PlayList.Add(playList);
                context.SaveChanges();
            }

            List<PlayListSongMap> playListSong = new List<PlayListSongMap>();
            foreach (var song in playListAddData.SelectedSongs)
            {
                var playlistSong = new PlayListSongMap()
                {
                    PlayListId = playList.Id,
                    SongId = song
                };
                playListSong.Add(playlistSong);
            }

            using (var context = new MyDBContext())
            {
                context.PlayListSongMap.AddRange(playListSong);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewPlayList(int id)
        {
            var playList = new PlayList();
            var songArtistMap = new List<SongArtistMap>();
            var playListSongMap = new List<PlayListSongMap>();
            var songs = new List<Song>();
            var artists = new List<Artist>();


            using (var context = new MyDBContext())
            {
                playList = context.PlayList.FirstOrDefault(a => a.Id == id);
                songArtistMap = context.SongArtistMap.ToList();
                playListSongMap = context.PlayListSongMap.ToList();
                songs = context.Song.ToList();
                artists = context.Artist.ToList();
            }


            var associatedSongs = playListSongMap
                    .Where(map => map.PlayListId == id)
                    .Join(songs, map => map.SongId, song => song.Id, (map, song) => song)
                    .ToList();


            var songData = new List<SongView>();

            foreach (var song in associatedSongs)
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

            var playListView = new PlayListView()
            {
                PlayList = playList,
                Songs = songData
            };

            return View(playListView);
        }
    }
}
