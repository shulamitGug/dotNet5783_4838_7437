using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// Unique ID of  OrderItem
        /// </summary>
        public int OrderItemId { get; set; }
        public string? ProductName { get; set; }
        /// <summary>
        /// Product ID number
        /// </summary>
        public int ProductId { get; set; }
        public double Price { get; set; }

        public int Amount { get; set; }
        /// <summary>
        /// The total price of the orderItems
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Overriding method of the ToString
        /// </summary>
        /// <returns>Returns a string describing a orderItem</returns>
        public override string ToString()
        {
            return ToolStringClass.ToStringProperty(this);
        }
    }
}
