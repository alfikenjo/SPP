using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class Referensi
    {
        public int Year { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }

        public int Total { get; set; }
        public int Diproses { get; set; }
        public int Selesai { get; set; }
        public int Ditolak { get; set; }

        public int BelumDiproses { get; set; }

    }
}
