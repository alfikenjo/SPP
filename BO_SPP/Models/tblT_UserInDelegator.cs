using System;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class tblT_UserInDelegator
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string DelegatorID { get; set; }

        private string fullname; public string Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string mobile; public string Mobile { get { return mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Fullname { get { return fullname; } set { fullname = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Mobile { get { return mobile; } set { mobile = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        public string Img { get; set; }
        public string Ekstension { get; set; }

        public string Delegators { get; set; }
        public string DelegatorName { get; set; }

        public string Action { get; set; }        
        public string CreatedBy { get; set; }       
        public string s_UpdatedOn { get; set; }
        public string Status { get; set; }

    }
}
