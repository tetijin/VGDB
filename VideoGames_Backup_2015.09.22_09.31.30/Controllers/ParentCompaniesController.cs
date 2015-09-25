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
    public class ParentCompaniesController : Controller
    {
        private VideoGamesEntities db = new VideoGamesEntities();

        // GET: ParentCompanies
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CompanySortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var companies = from c in db.ParentCompanies
                            select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(c => c.CompanyName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    companies = companies.OrderByDescending(c => c.CompanyName);
                    break;
                default:
                    companies = companies.OrderBy(c => c.CompanyName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(companies.ToPagedList(pageNumber, pageSize));

        }

        // GET: ParentCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentCompany parentCompany = db.ParentCompanies.Find(id);
            if (parentCompany == null)
            {
                return HttpNotFound();
            }
            return View(parentCompany);
        }

        // GET: ParentCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,CompanyName")] ParentCompany parentCompany)
        {
            if (ModelState.IsValid)
            {
                db.ParentCompanies.Add(parentCompany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parentCompany);
        }

        // GET: ParentCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentCompany parentCompany = db.ParentCompanies.Find(id);
            if (parentCompany == null)
            {
                return HttpNotFound();
            }
            return View(parentCompany);
        }

        // POST: ParentCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,CompanyName")] ParentCompany parentCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentCompany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parentCompany);
        }

        // GET: ParentCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentCompany parentCompany = db.ParentCompanies.Find(id);
            if (parentCompany == null)
            {
                return HttpNotFound();
            }
            return View(parentCompany);
        }

        // POST: ParentCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParentCompany parentCompany = db.ParentCompanies.Find(id);
            db.ParentCompanies.Remove(parentCompany);
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
