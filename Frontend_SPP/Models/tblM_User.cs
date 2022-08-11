using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class tblM_User
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string New_User_Password_Forgotten_ID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public string Register_Password { get; set; }
        public string Register_Password_Reentered { get; set; }

        public string Renew_Password { get; set; }
        public string Renew_Password_Reentered { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MobileTemp { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int? Mail_Verification { get; set; }
        public int? Mobile_Verification { get; set; }
        public string NIP { get; set; }
        public string Jabatan { get; set; }
        public Guid? ID_Unit { get; set; }
        public string Img { get; set; }
        public string Ekstension { get; set; }
        public int isActive { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public Nullable<DateTime> LastPasswordChanged { get; set; }

        public string Roles { get; set; }
        public string ID_Roles { get; set; }
        public string AdminUsername { get; set; }
        public IFormFile Foto { get; set; }
        public string FilePath { get; set; }

        public string Status { get; set; }
        public string s_Status { get; set; }
        public string Unit { get; set; }

        public string Action { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string s_UpdatedOn { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordVerifiy { get; set; }
        public int? EmailNotification { get; set; }

        public string captcha { get; set; }
        public string OTP { get; set; }

    }
}
