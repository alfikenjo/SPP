using System;
using Frontend_SPP.Common;

namespace Frontend_SPP.Models
{
    public class AuditTrail
    {
        public Guid? ID { get; set; }
        private string username; public string Username { get { return username; } set { username = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        public string Menu { get; set; }
        public string Halaman { get; set; }
        public string Item { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Datetime { get; set; }

    }
}
