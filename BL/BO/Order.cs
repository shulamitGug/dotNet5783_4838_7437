using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// Specific order id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the ordering customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The email address of the ordering customer
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// Residential address of the ordering customer
        /// </summary>
        public OrderStatus? Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<OrderItem?>? Items { get; set; }
        public double TotalPrice { get; set; }
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// Order Date
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// Shipping departure date
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// Arrival date of the shipment
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        public override string ToString()
        {
            return ToolStringClass.ToStringProperty(this);
        }

    }
}
