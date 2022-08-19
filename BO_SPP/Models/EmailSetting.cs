using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class EmailSetting
    {
        public string Action { get; set; }

        public string ID { get; set; }
        public string Tipe { get; set; }
        public string Subject { get; set; }
        public string Konten { get; set; }
        public string Parameter { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        public string UpdatedOn { get; set; }

    }
}
