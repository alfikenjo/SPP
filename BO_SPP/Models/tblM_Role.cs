using System;

namespace BO_SPP.Models
{
    public class tblM_Role
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Privileges { get; set; }
        public int Status { get; set; }
        public string s_Status { get; set; }

        public string Action { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string s_UpdatedOn { get; set; }

    }
}
