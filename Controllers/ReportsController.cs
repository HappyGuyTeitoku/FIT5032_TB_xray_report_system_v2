using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_TB_xray_report_system_v2.Models;

namespace FIT5032_TB_xray_report_system_v2.Controllers
{
    public class ReportsController : Controller
    {
        private TB_xray_systemsContainer db = new TB_xray_systemsContainer();

        // GET: Reports
        public ActionResult Index()
        {
            return View(db.ReportSet.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.ReportSet.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.ScreeningHistoryId = new SelectList(db.ScreeningHistorySet, "sh_id");
            ViewBag.PatientId = new SelectList(db.UserSet_Patient, "user_id", "user_fullname");
            ViewBag.MedicalProfessionalId = new SelectList(db.UserSet_MedicalProfessional, "user_id", "user_fullname");

            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "report_id,report_content")] Report report, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var newScreeningHistory = new ScreeningHistory();
                newScreeningHistory.sh_additional = "This SH is automatically created";
                newScreeningHistory.sh_datetime = DateTime.Now;
                db.ScreeningHistorySet.Add(newScreeningHistory);
                
                if (files != null) { 
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            // Process each uploaded file
                            string fileName = Path.GetFileName(file.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                            file.SaveAs(filePath);

                            var newImage = new ScreeningImage();

                            newImage.si_file = filePath;
                            newImage.ScreeningHistory_sh_id = newScreeningHistory.sh_id;

                            // Add the image as record into ScreeningImages
                            db.ScreeningImageSet.Add(newImage);
                        
                        }
                    }
                }
                db.ReportSet.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.ReportSet.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "report_id,report_content")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.ReportSet.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.ReportSet.Find(id);
            db.ReportSet.Remove(report);
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
