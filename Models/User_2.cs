using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_TB_xray_report_system_v2.Models
{
    public class User_2
    {
        public char User_id { get; set; }

        public string Username {  get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public User_status Status { get;set; }
    }

    public enum User_status { 
        Enabled = 1,
        Disabled = 2,
    }
}