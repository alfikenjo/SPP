using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class KuesionerDetail
    {
        public string Action { get; set; }
        public string IDKuesioner { get; set; }
        public string ID { get; set; }
        public string IDHeader { get; set; }
        public int Num { get; set; }
        public string InputType { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public string Options { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }

    }
}
