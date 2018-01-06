using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ValetService.DataObjects;
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
                 .Include(z => z.Zones).Include(e => e.Employees)
                 .Include(f => f.FeeRate).FirstOrDefault(d => d.Id == id);

            OrganizationDetailsViewModel organizationVM = new OrganizationDetailsViewModel
            {
                Organization = organization,
                FeeRate = organization.FeeRate
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
        public ActionResult Create([Bind(Include = "Id,Name,Type,Email,RegistrationNumber,Version,CreatedAt,UpdatedAt,Deleted")] Organization organization)
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
                    zone.OrganizationId = organizationId;
                    db.Zones.Add(zone);
                    var zn = db.Zones.Where(o => o.Organization.Id == organization.Id)
                        .Where(z => z.Name == zone.Name).FirstOrDefault();
                    if (zn == null)
                    {
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Details", new { id = organizationId });
        }

        [HttpPost, ActionName("AddTag")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTag(Tag tag, string organizationId)
        {
            if(ModelState.IsValid)
            {
                if(organizationId != null)
                {
                    var organization = db.Organizations.Find(organizationId);
                    tag.Id = Guid.NewGuid().ToString();
                    tag.Organization = organization;
                    tag.OrganizationId = organizationId;
                    db.Tags.Add(tag);
                    var tg = db.Tags.Where(o => o.Organization.Id == organization.Id)
                        .Where(t => t.TagNumber == tag.TagNumber).FirstOrDefault();
                    if (tg == null)
                    {
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Details", new { id = organizationId});
        }

        public ActionResult ViewTicketData()
        {
            List<Ticket> ticketList = db.Tickets.ToList();
            List<TicketViewModel> ticketVMList = new List<TicketViewModel>();

            foreach(Ticket item in ticketList)
            {
                Employee raiser = db.Employees.Find(item.TicketRaiserId);
                Employee closer = db.Employees.Find(item.TicketCloserId) ?? null;
                Employee assigner = db.Employees.Find(item.PickupAssignerId);

                TicketViewModel TVM = new TicketViewModel();
                TVM.TicketId = item.Id;
                TVM.TicketStatus = item.Status.ToString();
                TVM.TicketRaiser = raiser.FirstName + " " + raiser.Surname ?? "NULL";
                if(closer != null)
                TVM.TicketCloser = closer.FirstName + " " + closer.Surname;
                if (assigner != null)
                {
                    TVM.PickupAssigner = assigner.FirstName + " " + assigner.Surname;
                }
                TVM.PickupAccepted = item.PickupAccepted.ToString();
                TVM.Tag = db.Tags.Find(item.TagId).TagNumber.ToString();
                TVM.VehicleId = item.VehicleId;
                ticketVMList.Add(TVM);
            }

            return View(ticketVMList);
        }
        
        public ActionResult ViewVehicleData()
        {
            List<Vehicle> vehicleList = db.Vehicles.ToList();
            List<VehicleViewModel> vehicleVMList = new List<VehicleViewModel>();

            foreach(Vehicle item in vehicleList)
            {
                VehicleViewModel VVM = new VehicleViewModel();
                VVM.VehicleId = item.Id;
                VVM.LicencePlate = item.LicencePlateNumber;
                VVM.VehicleStatus = item.Status.ToString();
                if (item.ZoneId != null)
                {
                    VVM.Zone = db.Zones.Find(item.ZoneId).Name;
                }
                VVM.TicketId = item.TicketId;
                vehicleVMList.Add(VVM);
            }

            return View(vehicleVMList);
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
