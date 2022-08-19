using Microsoft.AspNetCore.Http;
using BO_SPP.Common;

namespace BO_SPP.Models
{
    public class PengaduanDetail
    {        
        public string ID { get; set; }    
        public string ID_Header { get; set; }

        private string nama; public string Nama { get { return nama; } set { nama = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string nomorhandphone; public string NomorHandphone { get { return nomorhandphone; } set { nomorhandphone = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string departemen; public string Departemen { get { return departemen; } set { departemen = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string jabatan; public string Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }

        public string enc_Nama { get { return nama; } set { nama = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_NomorHandphone { get { return nomorhandphone; } set { nomorhandphone = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Departemen { get { return departemen; } set { departemen = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Jabatan { get { return jabatan; } set { jabatan = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        private string email; public string Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string handphone; public string Handphone { get { return handphone; } set { handphone = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }
        private string createdby; public string CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Dec(value) : value; } }


        public string enc_Email { get { return email; } set { email = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_Handphone { get { return handphone; } set { handphone = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }
        public string enc_CreatedBy { get { return createdby; } set { createdby = !string.IsNullOrEmpty(value) ? aes.Enc(value) : value; } }

        public string _CreatedOn { get; set; }

        public IFormFile Upload { get; set; }
        public IFormFile UploadFileIdentitas { get; set; }

        public string FilepathFileIdentitas { get; set; }
        public string FileIdentitas { get; set; }
        public string FileIdentitas_Ekstension { get; set; }
        public string Action { get; set; }


    }
}
