using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGames.Models;

namespace VideoGames.Controllers
{
    public class GamesController : Controller
    {
        private VideoGamesEntities db = new VideoGamesEntities();

        // GET: Games
        /*
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Developer).Include(g => g.Genre).Include(g => g.Rating);
            return View(games.ToList());
        }
        */
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? id)
        {

            //In order for this to work we had to be sure to make the table headers in the Games/index.cshtml file @Html.Actionlinks.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewBag.DeveloperSortParm = sortOrder == "Developer" ? "developer_desc" : "Developer";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            // Let's get the model
            var games = from g in db.Games
                           select g;
            if(!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.GameTitle.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "title_desc":
                    games = games.OrderByDescending(g => g.GameTitle);
                    break;
                case "Genre":
                    games = games.OrderBy(g => g.GenreID);
                    break;
                case "genre_desc":
                    games = games.OrderByDescending(g => g.GenreID);
                    break;
                case "Rating":
                    games = games.OrderBy(g => g.RatingID);
                    break;
                case "rating_desc":
                    games = games.OrderByDescending(g => g.RatingID);
                    break;
                case "Developer":
                    games = games.OrderBy(g => g.DeveloperID);
                    break;
                case "developer_desc":
                    games = games.OrderByDescending(g => g.DeveloperID);
                    break;
                default:
                    games = games.OrderBy(g => g.GameTitle);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //Combine the model with the view and return
            return View(games.ToPagedList(pageNumber, pageSize));
        }


        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "RatingType");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,DeveloperID,GameTitle,GenreID,RatingID,MSRP")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", game.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", game.GenreID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "RatingType", game.RatingID);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", game.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", game.GenreID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "RatingType", game.RatingID);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,DeveloperID,GameTitle,GenreID,RatingID,MSRP")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeveloperID = new SelectList(db.Developers, "DeveloperID", "DeveloperName", game.DeveloperID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", game.GenreID);
            ViewBag.RatingID = new SelectList(db.Ratings, "RatingID", "RatingType", game.RatingID);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
