﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class attributes
    {
        public string code { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public override string ToString()
        {
            return $"{code} {name} {value}";
        }
    }
}
