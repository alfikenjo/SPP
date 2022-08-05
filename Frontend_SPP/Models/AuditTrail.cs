using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class AuditTrail
    {
        public Guid? ID { get; set; }
        public string Username { get; set; }
        public string Menu { get; set; }
        public string Halaman { get; set; }
        public string Item { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Datetime { get; set; }

    }
}
