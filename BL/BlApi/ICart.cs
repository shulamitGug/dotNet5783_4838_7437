using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public BO.Cart AddProduct(BO.Cart boCart, int id);
        public BO.Cart UpdateAmountOfProduct(BO.Cart boCart,int id, int amount);
        public int OrderConfirmation(BO.Cart boCart);
        public BO.Cart AddAndUpdate(BO.Cart boCart, int id, int amount);

    }
}
