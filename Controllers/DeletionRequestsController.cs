using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_TB_xray_report_system_v2.Models;

namespace FIT5032_TB_xray_report_system_v2.Controllers
{
    public class DeletionRequestsController : Controller
    {
        private TB_xray_systemsContainer db = new TB_xray_systemsContainer();

        // GET: DeletionRequests
        public ActionResult Index()
        {
            return View(db.DeletionRequestSet.ToList());
        }

        // GET: DeletionRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletionRequest deletionRequest = db.DeletionRequestSet.Find(id);
            if (deletionRequest == null)
            {
                return HttpNotFound();
            }
            return View(deletionRequest);
        }

        // GET: DeletionRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeletionRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dr_id,Patient_patient_id,dr_reason,dr_status")] DeletionRequest deletionRequest)
        {
            if (ModelState.IsValid)
            {
                db.DeletionRequestSet.Add(deletionRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deletionRequest);
        }

        // GET: DeletionRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletionRequest deletionRequest = db.DeletionRequestSet.Find(id);
            if (deletionRequest == null)
            {
                return HttpNotFound();
            }
            return View(deletionRequest);
        }

        // POST: DeletionRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dr_id,Patient_patient_id,dr_reason,dr_status")] DeletionRequest deletionRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deletionRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deletionRequest);
        }

        // GET: DeletionRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletionRequest deletionRequest = db.DeletionRequestSet.Find(id);
            if (deletionRequest == null)
            {
                return HttpNotFound();
            }
            return View(deletionRequest);
        }

        // POST: DeletionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeletionRequest deletionRequest = db.DeletionRequestSet.Find(id);
            db.DeletionRequestSet.Remove(deletionRequest);
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
