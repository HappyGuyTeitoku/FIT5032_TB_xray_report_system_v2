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
using System.Web.Security;
using System.Net;

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

        public ActionResult Verify(User login_user)
        {
            // Initialise Session Manager values
            Session["UserId"] = null;
            Session["Username"] = null;
            Session["UserRole"] = null;

            string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VisualStudiosProjects\FIT5032_TB_xray_report_system_v2\App_Data\FIT5032_TB_xray_report_system_v2.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection con = new SqlConnection(conString);

            cmd.Connection = con;
            con.Open();

            // Use parameterized query to prevent SQL injection
            cmd.CommandText = "SELECT * FROM UserSet WHERE user_username = @Username AND user_password = @Password";
            // Assuming login_user is an object of User class
            cmd.Parameters.AddWithValue("@Username", string.IsNullOrEmpty(login_user.user_username) ? (object)DBNull.Value : login_user.user_username);
            cmd.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(login_user.user_password) ? (object)DBNull.Value : login_user.user_password);

            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {

                login_user.user_id = sdr.GetInt32(sdr.GetOrdinal("user_id"));
                Session["UserId"] = login_user.user_id;
                Session["Username"] = sdr.GetString(sdr.GetOrdinal("user_username"));

                List<string> RoleTables = new List<string> { "UserSet_Administrator", "UserSet_MedicalProfessional", "UserSet_Patient" };

                foreach (string RoleTable in RoleTables)
                { 
                    sdr.Close();
                    cmd.CommandText = $"SELECT * FROM {RoleTable} WHERE user_id = @UserId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@UserId",login_user.user_id);

                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    { 
                        switch (RoleTable)
                        {
                            case "UserSet_Administrator":
                                Session["UserRole"] = "Administrator";
                                break;
                            case "UserSet_MedicalProfessional":
                                Session["UserRole"] = "MedicalProfessional";
                                break;
                            case "UserSet_Patient":
                                Session["UserRole"] = "Patient";
                                break;
                            default:
                                Session["UserRole"] = null;
                                break;
                        }
                        break;
                    }
                }

                con.Close();
                return View("LoginSuccess");
            }
            else
            {
                con.Close();
                return View("LoginFail");
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

        // GET: User
        [HttpGet]
        public ActionResult LogoutSuccess()
        {
            Session["UserId"] = null;
            Session["Username"] = null;
            Session["UserRole"] = null;
            return View();
        }

        // GET: User
        [HttpGet]
        public ActionResult Index() {

            var model = db.UserSet;
            return View(model);
        }

        //=============== Administrators Only: Account Editing ===============
        // GET: User for UpdateDetails (Modify/Edit)
        // ** For Administrators **
        [HttpGet]
        public ActionResult UpdateDetails(int? id)
        { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.UserSet.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserStatus = user.user_status;
            return View(user);
        }

        // POST: User/UpdateDetails/5
        // ** For Administrators **
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDetails([Bind(Include = "user_username,user_password,user_email,user_fullname,user_status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        //=============== Administrators Only: Account Editing ===============


        // GET: User for UpdateProfile (Modify/Edit)
        // ** For everyone to edit THEIR OWN user details **
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (Session["UserId"] != null)
            {
                int id = (int)Session["UserId"];
                #pragma warning disable CS0472
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.UserSet.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserStatus = user.user_status;
                return View(user);
            }
            return RedirectToAction("Index","Home");
        }

        // POST: User/UpdateProfile/5
        // ** For everyone to edit THEIR OWN user details **
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile([Bind(Include = "user_username,user_password,user_email,user_fullname,")] User updatedUser, string patient_address,string patient_phone, string patient_medicalhistory)
        {
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {
                    var existingUser = db.UserSet.Find(Session["UserId"]);

                    existingUser.user_username = updatedUser.user_username;
                    existingUser.user_password = updatedUser.user_password;
                    existingUser.user_email = updatedUser.user_email;
                    existingUser.user_fullname = updatedUser.user_fullname;

                    var user_patientdata = db.UserSet_Patient.Find(Session["UserId"]);
                    if (user_patientdata != null)
                    {
                        user_patientdata.patient_address = patient_address;
                        user_patientdata.patient_phone = patient_phone;
                        user_patientdata.patient_medical_history = patient_medicalhistory;
                    }

                    db.Entry(existingUser).State = EntityState.Modified;
                    db.Entry(user_patientdata).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View("UpdateProfile");
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User deleting_user = db.UserSet.Find(id);
            if (deleting_user == null)
            {
                return HttpNotFound();
            }
            return View(deleting_user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.UserSet.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            db.UserSet.Remove(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.UserSet.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (Session["UserRole"] != null)
            {
                return View(user);
            }
            return HttpNotFound();
        }


    }
}