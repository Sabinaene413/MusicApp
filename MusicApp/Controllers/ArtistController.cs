using Microsoft.AspNetCore.Mvc;
using MusicApp.Data.Entities;
using MusicApp.Data;

namespace MusicApp.Controllers
{
    public class ArtistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewArtists()
        {
            List<Artist> artists = new List<Artist>();
            using (var context = new MyDBContext())
            {
                artists = context.Artist.ToList();
            }
            return View(artists);
        }

        [HttpPost]
        public IActionResult RedirectToAddArtist()
        {
            return RedirectToAction("AddArtist");
        }

        public IActionResult EditArtist(int id)
        {
            var artist = new Artist();
            using (var context = new MyDBContext())
            {
                artist = context.Artist.FirstOrDefault(a => a.Id == id);
            }
            return View(artist);
        }

        public IActionResult UpdateArtist(Artist artist)
        {
            using (var context = new MyDBContext())
            {
                var existingArtist = context.Artist.FirstOrDefault(a => a.Id == artist.Id);
                existingArtist.Name = artist.Name;
                context.SaveChanges();
            }
            return RedirectToAction("ViewArtists");
        }

        public IActionResult DeleteArtist(int id)
        {
            using (var context = new MyDBContext())
            {
                var artist = context.Artist.FirstOrDefault(a => a.Id == id);

                context.SongArtistMap.RemoveRange(context.SongArtistMap.Where(x => x.ArtistId == artist.Id));
                context.SaveChanges();
                context.Artist.Remove(artist);
                context.SaveChanges();
            }

            return RedirectToAction("ViewArtists");
        }
        public IActionResult AddArtist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddArtist(Artist artistDetails)
        {
            using (var context = new MyDBContext())
            {

                context.Artist.Add(artistDetails);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }



    }
}
