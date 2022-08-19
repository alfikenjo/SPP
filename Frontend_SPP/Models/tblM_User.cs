using Microsoft.AspNetCore.Http;
using System;
using Frontend_SPP.Common;
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

        private string fullname; public string Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string mobile; public string Mobile { get { return mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string address; public string Address { get { return address; } set { address = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string nip; public string NIP { get { return nip; } set { nip = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string jabatan; public string Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string divisi; public string Divisi { get { return divisi; } set { divisi = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Mobile { get { return mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Address { get { return address; } set { address = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_NIP { get { return nip; } set { nip = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Divisi { get { return divisi; } set { divisi = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        public string MobileTemp { get; set; }
        public string Gender { get; set; }
        public int? Mail_Verification { get; set; }
        public int? Mobile_Verification { get; set; }
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
        public string s_UpdatedOn { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordVerifiy { get; set; }
        public int? EmailNotification { get; set; }

        public string captcha { get; set; }
        public string OTP { get; set; }

    }
}
