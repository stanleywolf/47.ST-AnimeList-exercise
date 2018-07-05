﻿using System.Linq;
using System.Web.Mvc;
using AnimeList.Models;

namespace AnimeList.Controllers
{
    [ValidateInput(false)]
    public class AnimeController : Controller
    {
        private AnimeListDbContext db = new AnimeListDbContext();

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var animes = db.Animes.ToList();
            return View(animes);
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anime anime)
        {
            db.Animes.Add(anime);
            db.SaveChanges();

            return Redirect("/");

        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            var anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }

            return View(anime);

        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Anime animeModel)
        {
            var animeDb = db.Animes.Find(id);
            db.Animes.Remove(animeDb);
            db.SaveChanges();
            return Redirect("/");

        }
    }
}