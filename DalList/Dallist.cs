using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal
{
     internal sealed class Dallist:IDal
    {
        public static IDal Instance { get; } = new Dallist();
        private Dallist() { }
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProducts();
        public IOrderItem OrderItem => new DalOrderItem();
    }
}
