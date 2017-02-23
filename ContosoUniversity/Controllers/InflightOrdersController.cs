using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class InflightOrdersController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: InflightOrders
        public ActionResult Index()
        {
            return View(db.InflightOrders.ToList());
        }

        // GET: InflightOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InflightOrder inflightOrder = db.InflightOrders.Find(id);
            if (inflightOrder == null)
            {
                return HttpNotFound();
            }
            return View(inflightOrder);
        }

        // GET: InflightOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InflightOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LogNumber,FleetNumber,QuotationNumber,UnitNumber,AmountOrder")] InflightOrder inflightOrder)
        {
            if (ModelState.IsValid)
            {
                db.InflightOrders.Add(inflightOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inflightOrder);
        }

        // GET: InflightOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InflightOrder inflightOrder = db.InflightOrders.Find(id);
            if (inflightOrder == null)
            {
                return HttpNotFound();
            }
            return View(inflightOrder);
        }

        // POST: InflightOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LogNumber,FleetNumber,QuotationNumber,UnitNumber,AmountOrder")] InflightOrder inflightOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inflightOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inflightOrder);
        }

        // GET: InflightOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InflightOrder inflightOrder = db.InflightOrders.Find(id);
            if (inflightOrder == null)
            {
                return HttpNotFound();
            }
            return View(inflightOrder);
        }

        // POST: InflightOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InflightOrder inflightOrder = db.InflightOrders.Find(id);
            db.InflightOrders.Remove(inflightOrder);
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
