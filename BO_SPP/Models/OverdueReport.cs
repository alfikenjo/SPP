using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class OverdueReport
    {
        public string ID { get; set; }
        public string Nomor { get; set; }
        public string Status { get; set; }
        public string Tanggal_Kirim { get; set; }
        public int Overdue { get; set; }     
        public string DelegatorName { get; set; }
    }
}
