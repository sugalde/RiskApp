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
using PagedList;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Created Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var transaction = from t in db.TransactionLogs
                              select t;
            if (!String.IsNullOrEmpty(searchString))
            {
                transaction = transaction.Where(s => s.RequestStatus.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "name_desc":
                    transaction = transaction.OrderByDescending(s => s.RequestStatus);
                    break;
                case "Date":
                    transaction = transaction.OrderBy(s => s.Created);
                    break;
                case "date_desc":
                    transaction = transaction.OrderByDescending(s => s.Created);
                    break;
                default:  // Name ascending 
                    transaction = transaction.OrderBy(s => s.QuotationID);
                    break;
            }

            int pageSize = 200;
            int pageNumber = (page ?? 1);
            return View(transaction.ToPagedList(pageNumber, pageSize));
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
                return RedirectToAction("RiskDetails","Risks", new { id = transactionLog.FleetNumber });
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

        //POST: RiskLists/ExportData
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = db.Risks.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Requestlist.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

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
