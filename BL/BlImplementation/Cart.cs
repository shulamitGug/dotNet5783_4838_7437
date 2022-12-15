using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart:BlApi.ICart
    {
        DalApi.IDal idal = new Dal.Dallist();
        /// <summary>
        /// add product to the cart we get
        /// </summary>
        /// <param name="boCart">cart</param>
        /// <param name="id">id of product that we want to add</param>
        /// <returns>the cart after update</returns>
        public BO.Cart AddProduct(BO.Cart boCart, int id)
        {
            DO.Product doProduct;
            try
            {
               doProduct = idal.Product.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
            if (boCart.Items == null)
                throw new Exception("cart is empty");
            //if this product exist in the cart -add one to the amount 
            BO.OrderItem? ord= boCart.Items!.FirstOrDefault(x=>x?.ProductId==id);
            if (ord != null)
            {
                ord.Amount += 1;
                ord.TotalPrice += doProduct.Price;
            }
            //if not exixt add the product to the cart
            else
            {
                BO.OrderItem item = new BO.OrderItem() { ProductId = id, ProductName = doProduct.Name, Amount = 1, TotalPrice = doProduct.Price };
                //item.orderItemId=
                boCart.Items.Add(item);
            }
            
            boCart.TotalPrice += doProduct.Price;
            return boCart;
        }
        /// <summary>
        /// this function update amount of products in cart
        /// </summary>
        /// <param name="boCart">cart with products</param>
        /// <param name="id">product id</param>
        /// <param name="amount"></param>
        /// <exception cref="Exception">if the produnt does not exist in cart</exception>
        public void UpdateAmountOfProduct(BO.Cart boCart, int id,int amount)
        {
            int itemAmount=0;
            DO.Product doProduct;
            try
            {
                 doProduct = idal.Product.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            //search the product in cart
            if (boCart.Items == null)
                throw new Exception("the cart is empty");
            BO.OrderItem? o = boCart.Items!.FirstOrDefault(x => x?.ProductId == id);
            if (o == null)
                throw new BO.NotExistBlException($"the product number {id} doesnot exist in this cart");
            itemAmount = o.Amount;
            if (amount > itemAmount)//check if there is enough in the stock
                if (amount - itemAmount > doProduct.InStock)
                    throw new BO.NotInStockException(id,doProduct.Name??"");
            //update

                   o.Amount = amount;
                
        }

    
        /// <summary>
        /// An action that checks the details of the cart and confirms it
        /// </summary>
        /// <param name="boCart">boCart </param>
        /// <exception cref="Exception"></exception>
        public void OrderConfirmation(BO.Cart boCart)
        {
            if (boCart.CustomerAdress == "") 
                throw new BO.NotEnoughDetailsException("customerAsress");
            if(boCart.CustomerEmail=="")
                throw new BO.NotEnoughDetailsException("customerEmail");
            if (boCart.CustomerName == "")
                throw new BO.NotEnoughDetailsException("customerName");
            //A loop that goes through the details of the cart and checks their correctness
            foreach (BO.OrderItem? item in boCart.Items!)
            {
                if (item?.Amount < 0)
                    throw new BO.NotValidException("amount");
                DO.Product doProduct;
                try
                {
                    doProduct = idal.Product.Get(item?.ProductId??0);
                }
                catch(DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                if (doProduct.InStock - item?.Amount < 0)
                    throw new BO.NotInStockException(doProduct.ID,doProduct.Name!);
            }
            DO.Order doOrder=new DO.Order() { CustomerAdress=boCart.CustomerAdress, CustomerName=boCart.CustomerName,CustomerEmail=boCart.CustomerEmail,ShipDate=DateTime.MinValue,DeliveryDate=DateTime.MinValue,OrderDate=DateTime.Now };
            int id;
            try
            {
                 id = idal.Order.Add(doOrder);
            }
            catch(DO.AlreadyExistException ex)
            {
                throw new BO.AlreadyExistBlException("order alredy exist cannot add- ", ex);
            }
            DO.OrderItem doOrderItem;
            //The loop creates a cart datatype of type do and adds it
            foreach (BO.OrderItem? boItem in boCart.Items)
            {
                DO.Product doProduct;
                try
                {
                    doProduct = idal.Product.Get(boItem?.ProductId??0);
                }
                catch (DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                doProduct.InStock -= boItem?.Amount??0;
                try
                {
                    idal.Product.Update(doProduct);
                }
                catch (DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                doOrderItem = new DO.OrderItem() { Amount = boItem!.Amount, ProductId = boItem.ProductId, OrderId = id, Price = boItem.TotalPrice };
                try
                {
                    idal.OrderItem.Add(doOrderItem);
                }
                catch(DO.AlreadyExistException ex)
                {
                    throw new BO.AlreadyExistBlException("orderItem is already exist", ex);
                }
            }
        }

    }
}
