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
    public class TransactionLogsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: TransactionLogs
        //public ActionResult Index()
        //{
        //    return View(db.TransactionLogs.ToList());
        //}

        //[HttpPost]
        public ActionResult Index(TransactionLog transactionLog)
        {
            var transaction = from t in db.TransactionLogs
                              select t;
            var transactionfilter = transaction.Where(t => t.FleetNumber.ToString().Contains(transactionLog.FleetNumber.ToString()) 
                              && t.QuotationID.ToString().Contains(transactionLog.QuotationID.ToString()));
            return View(transactionfilter.ToList());
        }

        // GET: TransactionLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionLog transactionLog = db.TransactionLogs.Find(id);
            if (transactionLog == null)
            {
                return HttpNotFound();
            }
            return View(transactionLog);
        }

        // GET: TransactionLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FleetNumber,QuotationID,CreditLineInitial,QuotationAmount")] TransactionLog transactionLog)
        {
            if (ModelState.IsValid)
            {
                
                transactionLog.CreatedBy = Environment.UserName;
                transactionLog.Created = DateTime.Now;
                Risk risk = db.Risks.Find(transactionLog.FleetNumber);
                transactionLog.CreditLineInitial = risk.CreditLine;
                transactionLog.OutstandingBalance = risk.OutstandingBalance;
                transactionLog.WorkProgress = risk.WorkProgress;
                transactionLog.InFlight = risk.InFlight;
                transactionLog.Sum = risk.Sum;
                if(transactionLog.QuotationAmount < (risk.CreditLine-(risk.OutstandingBalance + risk.InFlight + risk.WorkProgress)) && DateTime.Now < risk.ExpirationDate)
                {
                    transactionLog.RequestStatus = "approved";
                    //risk.OutstandingBalance = risk.OutstandingBalance - transactionLog.QuotationAmount;
                    risk.InFlight = risk.InFlight + transactionLog.QuotationAmount;
                    TryUpdateModel(risk);
                }
                else { transactionLog.RequestStatus = "rejected"; }
               
                db.TransactionLogs.Add(transactionLog);
                db.SaveChanges();
                return RedirectToAction("Index",transactionLog);
            }

            return View(transactionLog);
        }

        // GET: TransactionLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionLog transactionLog = db.TransactionLogs.Find(id);
            if (transactionLog == null)
            {
                return HttpNotFound();
            }
            return View(transactionLog);
        }

        // POST: TransactionLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FleetNumber,QuotationID,CreditLineInitial,QuotationAmount,CreatedBy,Created")] TransactionLog transactionLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionLog);
        }

        // GET: TransactionLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionLog transactionLog = db.TransactionLogs.Find(id);
            if (transactionLog == null)
            {
                return HttpNotFound();
            }
            return View(transactionLog);
        }

        // POST: TransactionLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionLog transactionLog = db.TransactionLogs.Find(id);
            db.TransactionLogs.Remove(transactionLog);
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
