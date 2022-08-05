using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class Model_Laporan
    {
        public string Tahun { get; set; }
        public string Kategori { get; set; }
        public string Kategori_EN { get; set; }
        public int Jumlah { get; set; }

        public string MM { get; set; }
        public string Bulan { get; set; }
        public string Bulan_EN { get; set; }


        public int KDP { get; set; }
        public int PUU { get; set; }
        public int PG { get; set; }
        public int WJ { get; set; }
        public int PKE { get; set; }
        public int AK { get; set; }


    }
}
