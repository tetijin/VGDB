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
    public class PlatformGamesController : Controller
    {
        private VideoGamesEntities db = new VideoGamesEntities();

        // GET: PlatformGames
        public ActionResult Index()
        {
            var platformGames = db.PlatformGames.Include(p => p.Game).Include(p => p.Platform);
            return View(platformGames.ToList());
        }

        // GET: PlatformGames/Details/5
        public ActionResult Details(int? platformID, int? gameID)
        {
            if ((platformID == null) && (gameID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlatformGame platformGame = db.PlatformGames.Find(gameID, platformID);
            if (platformGame == null)
            {
                return HttpNotFound();
            }
            return View(platformGame);
        }

        // GET: PlatformGames/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameTitle");
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName");
            return View();
        }

        // POST: PlatformGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlatformID,GameID,ReleaseDate")] PlatformGame platformGame)
        {
            if (ModelState.IsValid)
            {
                db.PlatformGames.Add(platformGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameTitle", platformGame.GameID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", platformGame.PlatformID);
            return View(platformGame);
        }

        // GET: PlatformGames/Edit/5
        public ActionResult Edit(int? platformID, int? gameID)
        {

            if ((platformID == null) && (gameID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlatformGame platformGame = db.PlatformGames.Find(gameID, platformID);
            if (platformGame == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameTitle", platformGame.GameID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", platformGame.PlatformID);
            return View(platformGame);
        }

        // POST: PlatformGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlatformID,GameID,ReleaseDate")] PlatformGame platformGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(platformGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameID = new SelectList(db.Games, "GameID", "GameTitle", platformGame.GameID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", platformGame.PlatformID);
            return View(platformGame);
        }

        // GET: PlatformGames/Delete/5
        public ActionResult Delete(int? platformID, int? gameID)
        {
            if ((platformID == null) && (gameID == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlatformGame platformGame = db.PlatformGames.Find(gameID, platformID);
            if (platformGame == null)
            {
                return HttpNotFound();
            }
            return View(platformGame);
        }

        // POST: PlatformGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? platformID, int? gameID)
        {
            PlatformGame platformGame = db.PlatformGames.Find(gameID, platformID);
            db.PlatformGames.Remove(platformGame);
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
