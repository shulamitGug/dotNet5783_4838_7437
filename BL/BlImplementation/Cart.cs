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
            DO.Products doProduct;
            bool exist = false;
            try
            {
               doProduct = idal.Product.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
            //if this product exist in the cart -add one to the amount 
            foreach(var item in boCart.items)
            {
                if(item.productId == id)
                {
                    exist = true;
                    item.amount += 1;
                    item.totalPrice += doProduct.Price;
                    break;
                }
            }
            //if not exixt add the product to the cart
            if(!exist)
            {
                BO.OrderItem item = new BO.OrderItem() { productId = id , productName = doProduct.Name, amount = 1 , totalPrice = doProduct.Price };
                //item.orderItemId=
                boCart.items.Add(item);
            }
            boCart.totalPrice += doProduct.Price;
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
            bool exist = false;
            int itemAmount=0;
            DO.Products doProduct;
            try
            {
                 doProduct = idal.Product.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            //search the product in cart
            foreach(var item in boCart.items)
            {
                if(item.productId == id)
                {
                    exist = true;
                    itemAmount = item.amount;
                }
            }
            if (!exist)
                throw new BO.NotExistBlException($"the product number {id} doesnot exist in this cart");
            if (amount > itemAmount)//check if there is enough in the stock
                if (amount - itemAmount > doProduct.InStock)
                    throw new BO.NotInStockException(id,doProduct.Name);
            //update
             foreach (var item in boCart.items)
            {
                if (item.productId == id)
                {
                   item.amount = amount;
                }
            }

        }
        /// <summary>
        /// An action that checks the details of the cart and confirms it
        /// </summary>
        /// <param name="boCart">boCart </param>
        /// <exception cref="Exception"></exception>
        public void OrderConfirmation(BO.Cart boCart)
        {
            if (boCart.customerAdress == "") 
                throw new BO.NotEnoughDetailsException("customerAsress");
            if(boCart.customerEmail=="")
                throw new BO.NotEnoughDetailsException("customerEmail");
            if (boCart.customerName == "")
                throw new BO.NotEnoughDetailsException("customerName");
            //A loop that goes through the details of the cart and checks their correctness
            foreach (var item in boCart.items)
            {
                if (item.amount < 0)
                    throw new BO.NotValidException("amount");
                DO.Products doProduct;
                try
                {
                    doProduct = idal.Product.Get(item.productId);
                }
                catch(DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                if (doProduct.InStock - item.amount < 0)
                    throw new BO.NotInStockException(doProduct.ID,doProduct.Name);
            }
            DO.Order doOrder=new DO.Order() { CustomerAdress=boCart.customerAdress, CustomerName=boCart.customerName,CustomerEmail=boCart.customerEmail,ShipDate=DateTime.MinValue,DeliveryDate=DateTime.MinValue,OrderDate=DateTime.Now };
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
            foreach (var boItem in boCart.items)
            {
                DO.Products doProduct;
                try
                {
                    doProduct = idal.Product.Get(boItem.productId);
                }
                catch (DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                doProduct.InStock -= boItem.amount;
                try
                {
                    idal.Product.Update(doProduct);
                }
                catch (DO.NotExistException ex)
                {
                    throw new BO.NotExistBlException("product does not exist-", ex);
                }
                doOrderItem = new DO.OrderItem() { Amount = boItem.amount, ProductId = boItem.productId, OrderId = id, Price = boItem.totalPrice };
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
