using Microsoft.AspNetCore.Http;
using System;

namespace BO_SPP.Models
{
    public class tblM_User
    {
        public string UserID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MobileTemp { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int? Mail_Verification { get; set; }
        public string NIP { get; set; }
        public string Jabatan { get; set; }
        public string Divisi { get; set; }
        public string Img { get; set; }
        public int isActive { get; set; }

        public string Roles { get; set; }
        public string Delegators { get; set; }
        public string ID_Roles { get; set; }
        public IFormFile Foto { get; set; }
        public string FilePath { get; set; }

        public string Status { get; set; }
        public string s_Status { get; set; }

        public string Action { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string s_UpdatedOn { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordVerifiy { get; set; }
        public int EmailNotification { get; set; }

        public string captcha { get; set; }
        public string OTP { get; set; }
        public string DelegatorID { get; set; }

    }
}
