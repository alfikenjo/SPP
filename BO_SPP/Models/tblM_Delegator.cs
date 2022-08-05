using System;

namespace BO_SPP.Models
{
    public class tblM_Delegator
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int isActive { get; set; }
        public string Status { get; set; }
        public int? CountMember { get; set; }

        public string Action { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string s_UpdatedOn { get; set; }

    }
}
