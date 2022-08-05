using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class M_FileEvidence
    {        
        public string ID { get; set; }    
        public string ID_Header { get; set; }
        public DateTime CreatedOn { get; set; }

        public string FilepathFileEvidence { get; set; }
        public string FileEvidence { get; set; }
        public string FileEvidence_Ekstension { get; set; }

    }
}
