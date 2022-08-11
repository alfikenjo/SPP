using System;

namespace BO_SPP.Models
{
    public class tblT_UserInDelegator
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string DelegatorID { get; set; }

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
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
