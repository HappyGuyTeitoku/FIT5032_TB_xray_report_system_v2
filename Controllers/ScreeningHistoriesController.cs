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
using FIT5032_TB_xray_report_system_v2.Models;

namespace FIT5032_TB_xray_report_system_v2.Controllers
{
    public class ScreeningHistoriesController : Controller
    {
        private TB_xray_systemsContainer db = new TB_xray_systemsContainer();

        // GET: ScreeningHistories
        public ActionResult Index()
        {
            var screeningHistorySet = db.ScreeningHistorySet.Include(s => s.MedicalProfessional).Include(s => s.Patient).Include(s => s.Report);
            return View(screeningHistorySet.ToList());
        }

        // GET: ScreeningHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreeningHistory screeningHistory = db.ScreeningHistorySet.Find(id);
            if (screeningHistory == null)
            {
                return HttpNotFound();
            }
            return View(screeningHistory);
        }

        // GET: ScreeningHistories/Create
        public ActionResult Create()
        {
            ViewBag.MedicalProfessional_user_id = new SelectList(db.UserSet_MedicalProfessional, "user_id", "user_username");
            ViewBag.Patient_user_id = new SelectList(db.UserSet_Patient, "user_id", "user_username");
            ViewBag.sh_id = new SelectList(db.ReportSet, "report_id", "report_content");
            return View();
        }

        // POST: ScreeningHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sh_id,sh_datetime,sh_additional,MedicalProfessional_user_id,Patient_user_id")] ScreeningHistory screeningHistory)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(screeningHistory.sh_additional)){
                    screeningHistory.sh_additional = "Filler content";
                }

                /*screeningHistory.MedicalProfessional_user_id = ;*/
                db.ScreeningHistorySet.Add(screeningHistory);
                try
                {
                    /*screeningHistory.ToString();*/
                    Debug.WriteLine(screeningHistory.ToString());
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var error in validationErrors.ValidationErrors)
                        {
                            Debug.WriteLine($"\n\nProperty: {error.PropertyName}, Error: {error.ErrorMessage}\n\n");
                        }
                    }

                    // Optionally, rethrow the exception or handle it accordingly
                    throw;
                }
                return RedirectToAction("Index");
            }

            ViewBag.MedicalProfessional_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.MedicalProfessional_user_id);
            ViewBag.Patient_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.Patient_user_id);
            ViewBag.sh_id = new SelectList(db.ReportSet, "report_id", "report_content", screeningHistory.sh_id);
            return View(screeningHistory);
        }

        // GET: ScreeningHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreeningHistory screeningHistory = db.ScreeningHistorySet.Find(id);
            if (screeningHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicalProfessional_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.MedicalProfessional_user_id);
            ViewBag.Patient_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.Patient_user_id);
            ViewBag.sh_id = new SelectList(db.ReportSet, "report_id", "report_content", screeningHistory.sh_id);
            return View(screeningHistory);
        }

        // POST: ScreeningHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sh_id,sh_datetime,sh_additional,MedicalProfessional_user_id,Patient_user_id")] ScreeningHistory screeningHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(screeningHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicalProfessional_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.MedicalProfessional_user_id);
            ViewBag.Patient_user_id = new SelectList(db.UserSet, "user_id", "user_username", screeningHistory.Patient_user_id);
            ViewBag.sh_id = new SelectList(db.ReportSet, "report_id", "report_content", screeningHistory.sh_id);
            return View(screeningHistory);
        }

        // GET: ScreeningHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreeningHistory screeningHistory = db.ScreeningHistorySet.Find(id);
            if (screeningHistory == null)
            {
                return HttpNotFound();
            }
            return View(screeningHistory);
        }

        // POST: ScreeningHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScreeningHistory screeningHistory = db.ScreeningHistorySet.Find(id);
            db.ScreeningHistorySet.Remove(screeningHistory);
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
