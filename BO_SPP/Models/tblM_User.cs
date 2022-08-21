using Microsoft.AspNetCore.Http;
using BO_SPP.Common;
using System;

namespace BO_SPP.Models
{
    public class tblM_User
    {
        public string UserID { get; set; }

        private string fullname; public string Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string mobile; public string Mobile { get { return !string.IsNullOrEmpty(mobile) ? mobile.Replace("-", "").TrimStart(new Char[] { '0' }) : mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string address; public string Address { get { return address; } set { address = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string nip; public string NIP { get { return nip; } set { nip = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string jabatan; public string Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string divisi; public string Divisi { get { return divisi; } set { divisi = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        //public string enc_Mobile { get { return mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Mobile { get { return !string.IsNullOrEmpty(mobile) ? mobile.Replace("-", "").TrimStart(new Char[] { '0' }) : mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Address { get { return address; } set { address = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_NIP { get { return nip; } set { nip = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Divisi { get { return divisi; } set { divisi = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        
        public string Gender { get; set; }
        public int? Mail_Verification { get; set; }

        public string Img { get; set; }
        public string Ekstension { get; set; }
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
