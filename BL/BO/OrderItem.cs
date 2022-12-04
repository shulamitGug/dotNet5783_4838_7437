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
        public int orderItemId { get; set; }
        public string productName { get; set; }
        /// <summary>
        /// Product ID number
        /// </summary>
        public int productId { get; set; }
        /// <summary>
        /// Price per unit
        /// </summary>
        /// <summary>
        /// Quantity
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// The total price of the orderItems
        /// </summary>
        public double totalPrice { get; set; }
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
