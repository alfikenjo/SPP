using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class Chart_Model
    {
        //public string name { get; set; }
        //public string type { get; set; }
        //public string color { get; set; }

        public string label { get; set; }
        public string backgroundColor { get; set; }

        public string borderColor { get; set; }
        public string borderWidth { get; set; }

        public List<int> data { get; set; }
    }
}
