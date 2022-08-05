using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class Laporan
    {
        public int Semua { get; set; }
        public int Terkirim { get; set; }
        public int Ditolak_Admin_SPP { get; set; }
        public int Diproses { get; set; }
        public int Ditolak_Delegator { get; set; }
        public int Dihentikan { get; set; }
        public int Ditindak_lanjut { get; set; }
        public int Selesai { get; set; }

        public string Delegator { get; set; }
        public int Masuk { get; set; }
        public int Proses { get; set; }
        public int Progress { get; set; }

    }
}
