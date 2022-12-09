using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        public OrderItem GetOrderItemByTwoValues(int product_id, int order_id);
        public IEnumerable<OrderItem?> GetOrderItemByOrder(int order_id);


    }
}
