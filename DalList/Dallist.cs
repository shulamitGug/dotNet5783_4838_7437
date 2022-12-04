using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal
{
    sealed public class Dallist:IDal
    {
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProducts();
        public IOrderItem OrderItem => new DalOrderItem();
    }
}
