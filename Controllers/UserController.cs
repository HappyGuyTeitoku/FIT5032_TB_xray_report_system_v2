using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIT5032_TB_xray_report_system_v2.Models;

namespace FIT5032_TB_xray_report_system_v2.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;

        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source = (LocalDb)\\MSSQLLocalDB; database = Database1; integrated security = SSPI;";
        }
        public ActionResult Verify(User_2 user)
        { 
            connectionString();
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
    }
}