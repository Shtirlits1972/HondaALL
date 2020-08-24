using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class CarTypeInfoGM
    {
        public string vehicle_id { get; set; }
        public string model_name { get; set; }
        public string xcardrs { get; set; }      //  xcardrs  'Кол-во дверей',
        public string dmodyr { get; set; }       // dmodyr  'Год выпуска',
        public string xgradefulnam { get; set; } //  xgradefulnam  Класс
        public string ctrsmtyp { get; set; }     //   ctrsmtyp VARCHAR(3) NOT NULL COMMENT 'Тип трансмиссии',
        public string cmftrepc { get; set; }     //  cmftrepc  'Страна производитель',
        public string carea { get; set; }        //  carea  'Страна рынок',

        public override string ToString()
        {
            return $" {vehicle_id} {model_name} ";
        }
    }
}
