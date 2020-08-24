using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class CarTypeInfo
    {
		public string vehicle_id { get; set; }   //   hmodtyp 'Код типа автомобиля',
		public string model_name { get; set; }   //  cmodnamepc  'Модель автомобиля',
		public string door { get; set; }      //  door  'Кол-во дверей',
		public string trans { get; set; }     //   Трансмиссия
		public string engine { get; set; }      //  Двигатель
		public string country { get; set; }        //  carea  'Страна рынок', // 
		public string year { get; set; }       // dmodyr  'Год выпуска',	


        public override bool Equals(object obj)
        {
            var item = obj as CarTypeInfo;

            if (item == null)
            {
                return false;
            }

            return this.vehicle_id.Equals(item.vehicle_id);
        }

        public override int GetHashCode()
        {
            return this.vehicle_id.GetHashCode();
        }


        public override string ToString()
        {
            return model_name;
        }
    }
}




