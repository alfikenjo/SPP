using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class PengaduanDetail
    {        
        public string ID { get; set; }    
        public string ID_Header { get; set; }
        public string Nama { get; set; }      
        public string NomorHandphone { get; set; }       
        public string Departemen { get; set; }
        public string Jabatan { get; set; }
        public string JenisPelanggaran { get; set; }

        public string _CreatedOn { get; set; }

        public IFormFile Upload { get; set; }
        public IFormFile UploadFileIdentitas { get; set; }

        public string FilepathFileIdentitas { get; set; }
        public string FileIdentitas { get; set; }
        public string FileIdentitas_Ekstension { get; set; }
        public string Action { get; set; }

        public string Email { get; set; }
        public string Handphone { get; set; }

    }
}
