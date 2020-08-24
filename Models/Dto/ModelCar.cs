using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class ModelCar
    {
        public string model_id { get; set; }  //  
        public string name { get; set; }
        public string seo_url { get; set; }


        public override bool Equals(object obj)
        {
            var item = obj as ModelCar;

            if (item == null)
            {
                return false;
            }

            return this.model_id.Equals(item.model_id);
        }

        public override int GetHashCode()
        {
            return this.model_id.GetHashCode();
        }
        public override string ToString()
        {
            return name;
        }
    }
}
