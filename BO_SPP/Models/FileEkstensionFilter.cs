using System;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class FileEkstensionFilter
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedOn { get; set; }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

    }
}
