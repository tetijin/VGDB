using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGames.Models;
using PagedList;

namespace VideoGames.Controllers
{
    public class DevelopersController : Controller
    {
        private VideoGamesEntities db = new VideoGamesEntities();

        // GET: Developers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.DevSortParm = String.IsNullOrEmpty(sortOrder) ? "dev_desc" : "";
            ViewBag.FoundedSortParm = sortOrder == "Founded" ? "founded_desc" : "Founded";
            ViewBag.GameSortParm = sortOrder == "Game" ? "game_desc" : "Game";
            // Original code, but we did it the other way to show how a controller gets the information from a model and combines it with a view to return to the user
            // return View(db.Developers.ToList());

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var developers = from d in db.Developers
                             select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                developers = developers.Where(d => d.DeveloperName.Contains(searchString));
            }


            //Option 2:
            //Lets' get the model
            //var developers = db.Developers.ToList();

            //Let's combine the model with the view and return this to the user
            switch (sortOrder)
            {
                case "dev_desc":
                    developers = developers.OrderByDescending(d => d.DeveloperName);
                    break;
                case "Founded":
                    developers = developers.OrderBy(d => d.DateEstablished);
                    break;
                case "founded_desc":
                    developers = developers.OrderByDescending(d => d.DateEstablished);
                    break;
                default:
                    developers = developers.OrderBy(d => d.DeveloperName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(developers.ToPagedList(pageNumber, pageSize));
        }
        

        // GET: Developers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //This is the model binder again
        public ActionResult Create([Bind(Include = "DeveloperID,DateEstablished,NumOfGames,DeveloperName")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                db.Developers.Add(developer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(developer);
        }

        // GET: Developers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //This is the model binder. What it does is limits the properties of the developer class that can be edited.
        public ActionResult Edit([Bind(Include = "DeveloperID,DateEstablished,NumOfGames,DeveloperName")] Developer developer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(developer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developer);
        }

        // GET: Developers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Developer developer = db.Developers.Find(id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Developer developer = db.Developers.Find(id);
            db.Developers.Remove(developer);
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
