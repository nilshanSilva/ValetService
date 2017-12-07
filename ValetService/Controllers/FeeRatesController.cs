using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ValetService.Models;

namespace ValetService.Controllers
{
    public class FeeRatesController : Controller
    {
        private MobileServiceContext db = new MobileServiceContext();

        // GET: FeeRates
        public ActionResult Index()
        {
            var feeRates = db.FeeRates.Include(f => f.Organization);
            return View(feeRates.ToList());
        }

        // GET: FeeRates/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeRate feeRate = db.FeeRates.Find(id);
            if (feeRate == null)
            {
                return HttpNotFound();
            }
            return View(feeRate);
        }

        // GET: FeeRates/Create
        public ActionResult Create(string organizationId)
        {
            ViewBag.Organization = db.Organizations.Find(organizationId);
            return View();
        }

        // POST: FeeRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitialPeriod,IntitalRate,NormalRate,Version,CreatedAt,UpdatedAt,Deleted")] FeeRate feeRate, string organizationId)
        {
            ModelState.Remove("Organization");
            if (ModelState.IsValid)
            {
                if(organizationId != null)
                {
                    var organization = db.Organizations.Find(organizationId);
                    feeRate.Id = Guid.NewGuid().ToString();
                    feeRate.Organization = organization;
                    feeRate.OrganizationId = organizationId;
                    var rate = db.FeeRates.Where(f => f.Organization.Id == organization.Id).FirstOrDefault();
                    if(rate == null)
                    {
                        db.FeeRates.Add(feeRate);
                        db.SaveChanges();
                    }
                }          
                return RedirectToAction("Details","Organizations", new { id = organizationId});
            }

            ViewBag.Id = new SelectList(db.Organizations, "Id", "Name", feeRate.Id);
            return View(feeRate);
        }

        // GET: FeeRates/Edit/5
        public ActionResult Edit(string id, string organizationId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeRate feeRate = db.FeeRates.Find(id);
            if (feeRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.Organization = db.Organizations.Find(organizationId);
            return View(feeRate);
        }

        // POST: FeeRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InitialPeriod,IntitalRate,NormalRate,Version,CreatedAt,UpdatedAt,Deleted")] FeeRate feeRate, string organizationId)
        {
            ModelState.Remove("Organization");
            if (ModelState.IsValid)
            {
                    var feeRateInDb = db.FeeRates.Find(feeRate.Id);
                    feeRateInDb.InitialPeriod = feeRate.InitialPeriod;
                    feeRateInDb.IntitalRate = feeRate.IntitalRate;
                    feeRateInDb.NormalRate = feeRate.NormalRate;
                feeRateInDb.Organization = db.Organizations.Find(organizationId);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Details", "Organizations", new { id = organizationId});
            }
            return View(feeRate);
        }

        // GET: FeeRates/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeRate feeRate = db.FeeRates.Find(id);
            if (feeRate == null)
            {
                return HttpNotFound();
            }
            return View(feeRate);
        }

        // POST: FeeRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FeeRate feeRate = db.FeeRates.Find(id);
            db.FeeRates.Remove(feeRate);
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
