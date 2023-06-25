using Microsoft.AspNetCore.Mvc;
using MusicApp.Data.Entities;
using MusicApp.Data;
using MusicApp.Data.Maps;

namespace MusicApp.Controllers
{
    public class TagController : Controller
    {
        public IActionResult ViewTags()
        {
            List<Tag> tags = new List<Tag>();
            using (var context = new MyDBContext())
            {
                tags = context.Tag.ToList();
            }

            return View(tags);
        }

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
            return RedirectToAction("ViewTags");
        }
    }
}
