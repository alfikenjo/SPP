using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string UpdatedBy { get; set; }


    }
}
