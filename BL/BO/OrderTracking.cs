using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public OrderStatus? status;
        public List<Tuple<DateTime?, string?>>? Tracking { get; set; }
        public override string ToString()
        {
            string str = "";
            foreach (var item in Tracking)
            {   str += "date: ";
                str += item.Item1+" text: ";
                str += item.Item2;
                str +="     ";
            }

            return str;
            //return ToolStringClass.ToStringProperty(this);
        }

    }
}
