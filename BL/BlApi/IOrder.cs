using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> GetOrders();
        public BO.Order GetOrderDetails(int id);
        public BO.Order updateSendingDate(int id);
        public BO.Order UpdateProvideDate(int id);
        public BO.OrderTracking StatusOrder(int id);
        //public void UpdateOrder()

    }
}
