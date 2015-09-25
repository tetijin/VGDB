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
    public class PlatformsController : Controller
    {
        private VideoGamesEntities db = new VideoGamesEntities();

        // GET: Platforms
        // The searchString parameter is used for the textbox that allows you to search platforms
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //These ViewBag properties allow you to share information from the controlloer to the view.
            ViewBag.CurrentSort = sortOrder; // Assigns the values of sortOrder to CurrentSort.
            ViewBag.PlatformSortParm = String.IsNullOrEmpty(sortOrder) ? "platform_desc" : "";
            ViewBag.CompanySortParm = sortOrder == "Company" ? "company_desc" : "Company";
            ViewBag.HardwareSortParm = sortOrder == "Hardware" ? "hardware_desc" : "Hardware";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //LINQ
            var platforms = db.Platforms.Include(p => p.Hardware).Include(p => p.ParentCompany);

            // This if statment is for the text box search in the platforms.index.cshtml
            // If the string is not null or empty then in platforms where platform name contains the search string
            // print out all platform names that contain the search string. 
            if(!String.IsNullOrEmpty(searchString))
            {
                platforms = platforms.Where(p => p.PlatformName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "platform_desc":
                    platforms = platforms.OrderByDescending(p => p.PlatformName);
                    break;
                case "Company":
                    platforms = platforms.OrderBy(p => p.CompanyID);
                    break;
                case "company_desc":
                    platforms = platforms.OrderByDescending(p => p.CompanyID);
                    break;
                case "Hardware":
                    platforms = platforms.OrderBy(p => p.HardwareID);
                    break;
                case "hardware_desc":
                    platforms = platforms.OrderByDescending(p => p.HardwareID);
                    break;
                default:
                    platforms = platforms.OrderBy(p => p.PlatformName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(platforms.ToPagedList(pageNumber, pageSize));
        }

        // GET: Platforms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // GET: Platforms/Create
        public ActionResult Create()
        {
            ViewBag.HardwareID = new SelectList(db.Hardwares, "HardwareID", "HardwareType");
            ViewBag.CompanyID = new SelectList(db.ParentCompanies, "CompanyID", "CompanyName");
            return View();
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlatformID,CompanyID,HardwareID,ReleaseDate,PlatformName")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                db.Platforms.Add(platform);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HardwareID = new SelectList(db.Hardwares, "HardwareID", "HardwareType", platform.HardwareID);
            ViewBag.CompanyID = new SelectList(db.ParentCompanies, "CompanyID", "CompanyName", platform.CompanyID);
            return View(platform);
        }

        // GET: Platforms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            ViewBag.HardwareID = new SelectList(db.Hardwares, "HardwareID", "HardwareType", platform.HardwareID);
            ViewBag.CompanyID = new SelectList(db.ParentCompanies, "CompanyID", "CompanyName", platform.CompanyID);
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlatformID,CompanyID,HardwareID,ReleaseDate,PlatformName")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                db.Entry(platform).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HardwareID = new SelectList(db.Hardwares, "HardwareID", "HardwareType", platform.HardwareID);
            ViewBag.CompanyID = new SelectList(db.ParentCompanies, "CompanyID", "CompanyName", platform.CompanyID);
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Platform platform = db.Platforms.Find(id);
            db.Platforms.Remove(platform);
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
