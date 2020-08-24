using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class FilterCarsResultJP
    {
        public List<header> header { get; set; }
        public List<CarTypeInfoJP> items { get; set; }
        public int cnt_items { get; set; }
        public int page { get; set; }
    }
}
