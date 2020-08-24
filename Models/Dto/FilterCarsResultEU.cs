using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class FilterCarsResultEU
    {
        public List<header> header { get; set; }
        public List<CarTypeInfoEU> items { get; set; }
        public int cnt_items { get; set; }
        public int page { get; set; }      
    }
}
