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

        public void UpdateAmountOfProduct(BO.Cart boCart,int id, int amount);
        public void OrderConfirmation(BO.Cart boCart);

    }
}
