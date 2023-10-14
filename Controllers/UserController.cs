using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIT5032_TB_xray_report_system_v2.Models;
using System.Diagnostics;

namespace FIT5032_TB_xray_report_system_v2.Controllers
{
    public class UserController : Controller
    {
        private TB_xray_systemsContainer db = new TB_xray_systemsContainer();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;

        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Verify(User_2 user)
        { 
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM UserSet where user_username = '"+user.Username+"' and password = '"+user.Password+"'";
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                con.Close();
                return View();
            }
            else
            {
                con.Close();
                return View();
            }
        }

        // GET: User/Create
        public ActionResult Create() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User userBase, string selectedRole)
        {
            if (ModelState.IsValid)
            {
                /*db.UserSet.Add(userBase);
                db.SaveChanges();*/
                
                Debug.WriteLine("\n"+userBase.ToString()+"\n\n"+selectedRole+"\n");

                switch (selectedRole)
                {
                    case "Administrator":
                        Administrator newAdmin = new Administrator();
                        newAdmin.user_id = userBase.user_id;
                        newAdmin.user_username = userBase.user_username;
                        newAdmin.user_password = userBase.user_password;
                        newAdmin.user_email = userBase.user_email;
                        newAdmin.user_fullname = userBase.user_fullname;
                        newAdmin.user_status = userBase.user_status;
                        // Admin ID is auto-generated, no need to pass from userBase.
                        db.UserSet_Administrator.Add(newAdmin);
                        break;

                    case "MedicalProfessional":
                        MedicalProfessional newMedic = new MedicalProfessional();
                        newMedic.user_id = userBase.user_id;
                        newMedic.user_username = userBase.user_username;
                        newMedic.user_password = userBase.user_password;
                        newMedic.user_email = userBase.user_email;
                        newMedic.user_fullname = userBase.user_fullname;
                        newMedic.user_status = userBase.user_status;
                        // MP ID is auto-generated, no need to pass from userBase.
                        db.UserSet_MedicalProfessional.Add(newMedic);
                        break;

                    case "Patient":
                        Patient newPatient = new Patient();
                        newPatient.user_id = userBase.user_id;
                        newPatient.user_username = userBase.user_username;
                        newPatient.user_password = userBase.user_password;
                        newPatient.user_email = userBase.user_email;
                        newPatient.user_fullname = userBase.user_fullname;
                        newPatient.user_status = userBase.user_status;
                        // Address, phone and medical history are unique to Patients
                        newPatient.patient_address = "Home Address";
                        newPatient.patient_phone = "0400000000";
                        newPatient.patient_medical_history = "Healthy";
                        // MP ID is auto-generated, no need to pass from userBase.
                        db.UserSet_Patient.Add(newPatient);
                        break;
                }
                db.Entry(userBase).State = EntityState.Detached;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}