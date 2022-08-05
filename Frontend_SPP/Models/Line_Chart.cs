using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend_SPP.Models
{
    public class Line_Chart
    {
        public string label { get; set; }
        public string borderColor { get; set; }
        public string tension { get; set; }

        public List<int> data { get; set; }
    }
}
