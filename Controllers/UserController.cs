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
            Session["UserRole"] = "NotFound";

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
                                Session["UserRole"] = "NotFound";
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
    }
}