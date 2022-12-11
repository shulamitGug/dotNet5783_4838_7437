﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class Order:BlApi.IOrder
    {
        IDal idal = new Dallist();
        /// <summary>
        /// Checking the status of the order
        /// </summary>
        /// <param name="order"> The order we would like to check</param>
        /// <returns>order status</returns>
        private BO.OrderStatus CheckStatus(DO.Order order)
        {
            if (order.DeliveryDate!=null)
                return (BO.OrderStatus)3;
            else if (order.ShipDate!=null)
                return  (BO.OrderStatus)2;
            else
                return (BO.OrderStatus)1;
        }
        /// <summary>
        /// Adding a product to the order
        /// </summary>
        /// <returns>List of ordered orders</returns>
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            double price = 0;
            int count = 0;
            IEnumerable<DO.Order> orders = idal.Order.GetAll();
            IEnumerable<DO.OrderItem> orderItems;
            List<BO.OrderForList> ordersForList=new List<BO.OrderForList?>();
            foreach (DO.Order order in orders)
            {
                try
                {
                    orderItems = idal.OrderItem.GetOrderItemByOrder(order.ID);
                }
                catch (Exception ex)
                {
                    throw new BO.NotExistBlException("orderitem is not exist-", ex);
                }
                foreach (DO.OrderItem item in orderItems)
                {
                    price+=item.Price*item.Amount;
                    count+=item.Amount;
                }
                BO.OrderForList boOrder = new BO.OrderForList() { CustomerName=order.CustomerName,ID=order.ID,TotalPrice=price,AmountOfItems=count,Status= CheckStatus(order) };
                ordersForList.Add(boOrder);
            }
            return ordersForList;
        }
        /// <summary>
        /// The function returns order details according to order id
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns> A data type that contains the order details</returns>
        /// <exception cref="Exception"></exception>
        public BO.Order GetOrderDetails(int id)
        {
            double price = 0;
            if (id < 0)
                throw new BO.NotValidException("id canot be negative");
            DO.Order doOrder;
            try
            {
                 doOrder = idal.Order.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("notExist ", ex);
            }
            BO.Order boOrder = new BO.Order() { ID=doOrder.ID,CustomerName=doOrder.CustomerName,ShipDate=doOrder.ShipDate,DeliveryDate=doOrder.DeliveryDate,OrderDate=doOrder.OrderDate,CustomerEmail=doOrder.CustomerEmail,CustomerAdress=doOrder.CustomerAdress,Status= CheckStatus(doOrder) };
            boOrder.Items=new List<BO.OrderItem>();
            IEnumerable<DO.Product> productsList=idal.Product.GetAll();
            IEnumerable<DO.OrderItem> doItems = idal.OrderItem.GetOrderItemByOrder(id)?? null;
            //A loop that goes through the order details and enters them into the bo data type
            foreach (DO.OrderItem item in doItems)
            {
                BO.OrderItem boItem = new BO.OrderItem() { OrderItemId = item.ID, ProductId = item.ProductId, TotalPrice = item.Price * item.Amount, Amount = item.Amount };
                //A loop that goes through the products in the order and updates the price of the order
                foreach (var product in productsList)
                {
                    if(product.ID==item.ProductId)
                    {
                        boItem.ProductName = product.Name;
                        break;
                    }
                }
                boOrder.Items.Add(boItem);
                price += boItem.TotalPrice;
            }
            boOrder.TotalPrice = price;
            return boOrder;
        }
        /// <summary>
        /// An action that updates the status of the order and returns the updated order
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Order with updated status </returns>
        /// <exception cref="Exception"> If the id is negative an exception is thrown</exception>
        public BO.Order updateSendingDate(int id)
        {
            if(id<0)
                throw new BO.NotValidException("ID cannot be negative");
            DO.Order doOrder;
            try
            {
                doOrder = idal.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("notExist ", ex);
            }
            if (doOrder.ShipDate.Date<DateTime.Now.Date)
            {
                doOrder.ShipDate=DateTime.Now;
                idal.Order.Update(doOrder);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.Status =(BO.OrderStatus)2;
            return newOrder;
        }
        /// <summary>
        /// An action that updates the delivery date and returns the order with an updated delivery date
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Returns an order with an updated status </returns>
        /// <exception cref="Exception">If the id is negative an exception is thrown</exception>
        public BO.Order UpdateProvideDate(int id)
        {
            if (id < 0)
                throw new Exception("canot be negative");
            DO.Order doOrder;
            try
            {
                doOrder = idal.Order.Get(id);
            }
            catch(Exception ex)
            {
                throw new BO.NotExistBlException("order not exist", ex);
            }
            if (doOrder.DeliveryDate.Date < DateTime.Now.Date)
            {
                doOrder.DeliveryDate = DateTime.Now;
                idal.Order.Update(doOrder);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.Status = (BO.OrderStatus)3;
            return newOrder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns> </returns>
        public BO.OrderTracking StatusOrder(int id)
        {
            DO.Order doOrder;
            try
            {
                doOrder = idal.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("order not exist", ex);
            }
            BO.OrderTracking trackingOrder = new BO.OrderTracking() { ID = doOrder.ID, Status = CheckStatus(doOrder) };
            trackingOrder.Tracking = new List<Tuple<DateTime, string>>();
            Tuple<DateTime, string> newTuple = new Tuple<DateTime, string>(doOrder.OrderDate, "approved");
            trackingOrder.Tracking.Add(newTuple);
            if (doOrder.ShipDate.Date<DateTime.MinValue)
            {
                newTuple = new Tuple<DateTime, string>(doOrder.ShipDate, "sent");
                trackingOrder.Tracking.Add(newTuple);
            }
             if (doOrder.DeliveryDate.Date <DateTime.MinValue)
            {
                newTuple = new Tuple<DateTime, string>(doOrder.DeliveryDate, "provided");
                trackingOrder.Tracking.Add(newTuple);
            }
            return trackingOrder;
        }


    }
}