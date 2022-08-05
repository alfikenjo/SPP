using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class MTanggapan
    {        
        public string ID { get; set; }

        public string JenisPengaduan { get; set; }
        public string IDPengaduan { get; set; }
        public string TipePengirim { get; set; }
        public string Email { get; set; }
        public string Nama { get; set; }
        public string Tanggapan { get; set; }
        public string FileLampiran { get; set; }
        public string FileLampiran_Ekstension { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int IsRead { get; set; }
        public Nullable<DateTime> ReadOn { get; set; }
        public string _Createdon { get; set; }
        public string s_CreatedOn { get; set; }

        public IFormFile UploadFileLampiran { get; set; }
        public string FilepathFileLampiran { get; set; }

        public string Msg_Title { get; set; }
        public string Nomor { get; set; }
        public string Msg_Link { get; set; }

        public string DelegatorName { get; set; }


    }
}
