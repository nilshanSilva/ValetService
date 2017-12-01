using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ValetService.Models;
using ValetService.ViewModels;

namespace ValetService.Controllers
{
    public class OrganizationsController : Controller
    {
        private MobileServiceContext db = new MobileServiceContext();

        // GET: Organizations
        public ActionResult Index()
        {
            var organizations = db.Organizations.Include(o => o.FeeRate);
            return View(organizations.ToList());
        }

        // GET: Organizations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Include(t => t.Tags)
                 .Include(z => z.Zones).Include(e => e.Employees).SingleOrDefault(d => d.Id == id);

            OrganizationDetailsViewModel organizationVM = new OrganizationDetailsViewModel
            {
                Organization = organization
            };

            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organizationVM);
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.FeeRates, "Id", "Id");
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,Email,Version,CreatedAt,UpdatedAt,Deleted")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.Id = Guid.NewGuid().ToString();
                db.Organizations.Add(organization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.FeeRates, "Id", "Id", organization.Id);
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.FeeRates, "Id", "Id", organization.Id);
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,Email,Version,CreatedAt,UpdatedAt,Deleted")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.FeeRates, "Id", "Id", organization.Id);
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Organization organization = db.Organizations.Find(id);
            db.Organizations.Remove(organization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AddZone")]
        [ValidateAntiForgeryToken]
        public ActionResult AddZone(Zone zone, string organizationId)
        {
            if (ModelState.IsValid)
            {
                if (organizationId != null)
                {
                    var organization = db.Organizations.Find(organizationId);
                    zone.Id = Guid.NewGuid().ToString();
                    zone.Organization = organization;
                    db.Zones.Add(zone);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id = organizationId });
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
