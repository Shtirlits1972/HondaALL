using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaALL.Models.Dto
{
    public class values
    {
        public string filter_item_id { get; set; }
        public string name { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as values;

            if (item == null)
            {
                return false;
            }

            return this.filter_item_id.Equals(item.filter_item_id);
        }

        public override int GetHashCode()
        {
            return this.filter_item_id.GetHashCode();
        }


        public override string ToString()
        {
            return name;
        }
    }
}
