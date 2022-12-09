using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string? customerName { get; set; }
        public OrderStatus? status { get; set; }
        public int amountOfItems { get; set; }
        public double totalPrice { get; set; }
        public override string ToString()
        {
            return ToolStringClass.ToStringProperty(this);
        }
    }
}
