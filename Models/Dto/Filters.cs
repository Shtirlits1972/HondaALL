using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class Filters
    {
        public string filter_id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public List<values> values { get; set; } = new List<values>();
        public override string ToString()
        {
            return name;
        }
    }
}
