using Microsoft.AspNetCore.Http;
using System;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class MTanggapan
    {        
        public string ID { get; set; }

        public string JenisPengaduan { get; set; }
        public string IDPengaduan { get; set; }
        public string TipePengirim { get; set; }

        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string nama; public string Nama { get { return nama; } set { nama = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string tanggapan; public string Tanggapan { get { return tanggapan; } set { tanggapan = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string createdby; public string CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Nama { get { return nama; } set { nama = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Tanggapan { get { return tanggapan; } set { tanggapan = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        public string FileLampiran { get; set; }
        public string FileLampiran_Ekstension { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
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
