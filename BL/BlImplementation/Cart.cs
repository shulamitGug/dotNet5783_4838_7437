﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class Cart:BlApi.ICart
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        /// <summary>
        /// add product to the cart we get
        /// </summary>
        /// <param name="boCart">cart</param>
        /// <param name="id">id of product that we want to add</param>
        /// <returns>the cart after update</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Cart AddProduct(BO.Cart boCart, int id)
        {
            DO.Product doProduct;
            try
            {
               doProduct = idal!.Product.Get(id);
            }

            catch(Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
            //if this product exist in the cart -add one to the amount 
            BO.OrderItem? ord= boCart.Items?.FirstOrDefault(x=>x?.ProductId==id);
            
            if (ord != null)
            {
                ord.Amount += 1;
                ord.TotalPrice += doProduct.Price;
            }
            //if not exixt add the product to the cart
            else
            {
                if (boCart.Items == null)
                    boCart.Items = new List<BO.OrderItem?>();
                BO.OrderItem boItem = new BO.OrderItem() { ProductId = id,Price=doProduct.Price, ProductName = doProduct.Name, Amount = 1, TotalPrice = doProduct.Price };
                boCart.Items.Add(boItem);
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
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Cart UpdateAmountOfProduct(BO.Cart boCart, int id, int amount)
        {
            BO.Cart cart = new BO.Cart();
            int itemAmount = 0;
            DO.Product doProduct;
            try
            {
                doProduct = idal!.Product.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            //search the product in cart
            if (boCart.Items == null)
                throw new Exception("the cart is empty");

            BO.OrderItem? orderItem = boCart.Items.FirstOrDefault(x => x?.ProductId == id);

            if (orderItem == null)
                throw new BO.NotExistBlException($"the product number {id} doesnot exist in this cart");
            itemAmount = orderItem.Amount;

            if (amount > itemAmount)//check if there is enough in the stock

                if (amount > doProduct.InStock)
                {
                    orderItem.Amount = doProduct.InStock;
                    boCart.TotalPrice -= orderItem.TotalPrice;
                    orderItem.TotalPrice = doProduct.InStock * doProduct.Price;
                    boCart.TotalPrice += orderItem.TotalPrice;
                    throw new BO.NotInStockException(id, doProduct.Name ?? "");

                }
            if(amount==0)
            {
                boCart.TotalPrice -= orderItem.TotalPrice;
                boCart.Items.RemoveAll(x => x?.ProductId == id);
            }
            else
            {
                orderItem.Amount = amount;
                boCart.TotalPrice -= orderItem.TotalPrice;
                orderItem.TotalPrice = amount * doProduct.Price;
                boCart.TotalPrice += orderItem.TotalPrice;
            }
            //update
           
             cart.TotalPrice=boCart.TotalPrice;
            cart.Items = new(boCart.Items);
            cart.CustomerEmail =boCart.CustomerEmail;
            cart.CustomerName =boCart.CustomerName;
            cart.CustomerAdress =boCart.CustomerAdress;
            return cart;
        }

        public BO.Cart AddAndUpdate(BO.Cart boCart, int id, int amount)
        {
            return UpdateAmountOfProduct(AddProduct(boCart, id), id, amount);
        }
        /// <summary>
        /// An action that checks the details of the cart and confirms it
        /// </summary>
        /// <param name="boCart">boCart </param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public int OrderConfirmation(BO.Cart boCart)
        {
            if (boCart.CustomerAdress == null)
                throw new BO.NotEnoughDetailsException("customerAsress");
            if (boCart.CustomerEmail == null)
                throw new BO.NotEnoughDetailsException("customerEmail");
            if (boCart.CustomerName == null)
                throw new BO.NotEnoughDetailsException("customerName");
            int negativeAmount = boCart.Items!.Count(x => x!.Amount < 0);
            if (!boCart.CustomerEmail!.Contains("@"))
                throw new BO.NotValidException("mail");
                if(negativeAmount > 0)
                throw new BO.NotValidException("amount");
            try
            {
                boCart.Items!.FindAll(x => idal!.Product.Get(x!.ProductId).InStock - x.Amount >= 0 ? true : throw new BO.NotInStockException(x.ProductId, x.ProductName!));
                DO.Order doOrder = new DO.Order() { CustomerAdress = boCart.CustomerAdress, CustomerName = boCart.CustomerName, CustomerEmail = boCart.CustomerEmail, ShipDate = null, DeliveryDate = null, OrderDate = DateTime.Now };
                int id = idal!.Order.Add(doOrder);
                var allItems = from item in boCart.Items
                        let product = idal!.Product.Get(item.ProductId)
                        let newProd = new DO.Product { ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock - item.Amount, CategoryP = product.CategoryP }
                        let updateAmount = UpdateAmountDal(newProd)
                        select new DO.OrderItem() { Amount = item!.Amount, ProductId = item.ProductId, OrderId = id, Price = newProd.Price };
                allItems.All(x => idal.OrderItem.Add(x) > 0 ? true : false);
                return id;
            }
            catch (DO.AlreadyExistException ex)
            {
                throw new BO.AlreadyExistBlException("order alredy exist cannot add- ", ex);
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist-", ex);
            }
        }
        /// <summary>
        /// help function to add to cart and update amount
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        private bool UpdateAmountDal(DO.Product prod)
        {
            idal!.Product.Update(prod);
            return true;
        }
 

    }
}
