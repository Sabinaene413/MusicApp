using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Data.Entities;
using MusicApp.Data.ForViews;
using MusicApp.Data.Maps;
using MusicApp.Models;
using System.Diagnostics;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

  
        //playlist

        //playlist end

        //tag
        public IActionResult AddTag()
        {
            List<Song> songs = new List<Song>();
            using (var context = new MyDBContext())
            {
                songs = context.Song.ToList();
            }
            return View(songs);
        }

        [HttpPost]
        public IActionResult AddTag(AddPlayListData TagAddData)
        {
            Tag tag = new Tag()
            {
                Name = TagAddData.Name,
            };

            using (var context = new MyDBContext())
            {
                context.Tag.Add(tag);
                context.SaveChanges();
            }

            List<TagSongMap> TagSong = new List<TagSongMap>();
            foreach (var song in TagAddData.SelectedSongs)
            {
                var tagSong = new TagSongMap()
                {
                    TagId = tag.Id,
                    SongId = song
                };
                TagSong.Add(tagSong);
            }

            using (var context = new MyDBContext())
            {
                context.TagSongMap.AddRange(TagSong);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //tag end

        //musictype
        public IActionResult AddMusicType()
        {
            var musicTypeDataForAdd = new MusicTypeDataForAdd();
            using (var context = new MyDBContext())
            {
                musicTypeDataForAdd.Artists = context.Artist.ToList();
                musicTypeDataForAdd.Songs = context.Song.ToList();
                musicTypeDataForAdd.PlayLists = context.PlayList.ToList();
            }

            return View(musicTypeDataForAdd);
        }

        [HttpPost]
        public IActionResult AddMusicType(MusicTypeDataForAdd musicTypeData)
        {
            var musicType = new MusicType()
            {
                Name = musicTypeData.Name,
            };

            using (var context = new MyDBContext())
            {
                context.MusicType.Add(musicType);
                context.SaveChanges();
            }

            var musicTypeArtists = new List<MusicTypeArtistMap>();
            foreach (var artist in musicTypeData.SelectedArtists)
            {
                var musicTypeArtist = new MusicTypeArtistMap()
                {
                    MusicTypeId = musicType.Id,
                    ArtistId = artist,
                };
                musicTypeArtists.Add(musicTypeArtist);
            }

            var musicTypePlayLists = new List<MusicTypePlayListMap>();
            foreach (var playList in musicTypeData.SelectedPlayLists)
            {
                var musicTypePlayList = new MusicTypePlayListMap()
                {
                    MusicTypeId = musicType.Id,
                    PlayListId = playList,
                };
                musicTypePlayLists.Add(musicTypePlayList);
            }

            var musicTypeSongs = new List<MusicTypeSongMap>();
            foreach (var song in musicTypeData.SelectedSongs)
            {
                var musicTypeSong = new MusicTypeSongMap()
                {
                    MusicTypeId = musicType.Id,
                    SongId = song,
                };
                musicTypeSongs.Add(musicTypeSong);
            }

            using (var context = new MyDBContext())
            {
                context.MusicTypeArtistMap.AddRange(musicTypeArtists);
                context.MusicTypePlayListMap.AddRange(musicTypePlayLists);
                context.MusicTypeSongMap.AddRange(musicTypeSongs);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}