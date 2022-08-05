using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BO_SPP.Models
{
    public class KuesionerValue
    {
        public string ID { get; set; }
        public string IDPengaduan { get; set; }
        public string Title { get; set; }
        public int Num { get; set; }
        public string InputType { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
        public string Options { get; set; }
        public string InputValue { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}
