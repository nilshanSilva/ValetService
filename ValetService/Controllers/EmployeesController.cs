using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ValetService.Models;

namespace ValetService.Controllers
{
    public class EmployeesController : Controller
    {
        private MobileServiceContext db = new MobileServiceContext();

        // GET: Employees
        public ActionResult Index(string organizationId)
        {
            var employees = db.Employees.Include(o => o.Organization)
                .Where(e => e.Organization.Id == organizationId).ToList();
            ViewBag.Organization = db.Organizations.Find(organizationId);
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create(string organizationId)
        {
            ViewBag.Organization = db.Organizations.Find(organizationId);
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,Surname,Gender,BirthDate,IDCardNumber,Email,Password,UserLevel")] Employee employee, string organizationId)
        {
            ModelState.Remove("Organization"); //Employee is the Dependent End, the Organization (Principal End) will be added here
            if (ModelState.IsValid)
            {
                if (organizationId != null)
                {
                    var organization = db.Organizations.Find(organizationId);
                    employee.Organization = organization;
                    employee.Id = Guid.NewGuid().ToString();
                    db.Employees.Add(employee);
                    var emp = db.Employees.Where(e => e.IDCardNumber == employee.IDCardNumber).FirstOrDefault();
                    if (emp == null)
                    {
                        db.SaveChanges();
                    }
                }
                
                return RedirectToAction("Index", new { organizationId = organizationId});
            }

            return RedirectToAction("Create", new { organizationId = organizationId });
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,Surname,Gender,BirthDate,IDCardNumber,Email,Password,UserLevel,Version,CreatedAt,UpdatedAt,Deleted")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
