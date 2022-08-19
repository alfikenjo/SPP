using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class Kuesioner
    {
        public string Action { get; set; }
        public string ID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string UpdatedOn { get; set; }
        private string updatedby; public string UpdatedBy { get { return updatedby; } set { updatedby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }


    }
}
