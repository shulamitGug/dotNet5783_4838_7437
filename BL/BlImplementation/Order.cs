﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        /// <summary>
        /// Checking the status of the order
        /// </summary>
        /// <param name="order"> The order we would like to check</param>
        /// <returns>order status</returns>
       
        private BO.OrderStatus CheckStatus(DO.Order order)
        {
            if (order.DeliveryDate != null)
                return (BO.OrderStatus)3;

            else if (order.ShipDate != null)
                return (BO.OrderStatus)2;

            else
                return (BO.OrderStatus)1;
        }
        /// <summary>
        /// Adding a product to the order
        /// </summary>
        /// <returns>List of ordered orders</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.OrderForList> GetOrders()
        {
            try
            {
                IEnumerable<DO.Order?> orders = idal!.Order.GetAll();
                var ordersForList = from order in orders
                                    let orderItems = idal.OrderItem.GetAll(orditem => orditem?.OrderId == order?.ID)
                                    let amount = orderItems.Sum(o => ((DO.OrderItem)o!).Amount)
                                    let totalPrice = orderItems.Sum(o => ((DO.OrderItem)o!).Amount * ((DO.OrderItem)o!).Price)
                                    select new BO.OrderForList
                                    {
                                        ID = ((DO.Order)order!).ID,
                                        CustomerName = ((DO.Order)order!).CustomerName,
                                        AmountOfItems = amount,
                                        TotalPrice = totalPrice,
                                        Status = (((DO.Order)order!).DeliveryDate != null && ((DO.Order)order!).DeliveryDate < DateTime.Now) ?
                                     BO.OrderStatus.provided : ((DO.Order)order!).ShipDate != null && ((DO.Order)order!).ShipDate < DateTime.Now ?
                                     BO.OrderStatus.sent : BO.OrderStatus.approved
                                    };
                return ordersForList;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("order doesnot exist", ex);
            }
        }

        /// <summary>
        /// The function returns order details according to order id
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns> A data type that contains the order details</returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order GetOrderDetails(int id)
        {
            if (id < 0)
                throw new BO.NotValidException("id canot be negative");
            DO.Order doOrder;
            try
            {
                doOrder = idal!.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("notExist ", ex);
            }
            BO.Order boOrder = new BO.Order() { ID = doOrder.ID, CustomerName = doOrder.CustomerName, ShipDate = doOrder.ShipDate, DeliveryDate = doOrder.DeliveryDate, OrderDate = doOrder.OrderDate, CustomerEmail = doOrder.CustomerEmail, CustomerAdress = doOrder.CustomerAdress, Status = CheckStatus(doOrder) };
            boOrder.Items = new List<BO.OrderItem?>();
            IEnumerable<DO.Product?> productsList = idal!.Product.GetAll();
            IEnumerable<DO.OrderItem?> doItems = idal!.OrderItem.GetAll(x => x?.OrderId == id);
            //A loop that goes through the order details and enters them into the bo data type
            IEnumerable<BO.OrderItem?> allItems = from itemQuery in doItems
                                                  let prodName = productsList.FirstOrDefault(x => x?.ID == ((DO.OrderItem)itemQuery!).ProductId)?.Name
                                                  select new BO.OrderItem
                                                  {
                                                      OrderItemId = ((DO.OrderItem)itemQuery!).ID,
                                                      ProductId = ((DO.OrderItem)itemQuery!).ProductId,
                                                      ProductName = prodName,
                                                      Price=((DO.OrderItem)itemQuery!).Price,
                                                      TotalPrice = ((DO.OrderItem)itemQuery!).Price * ((DO.OrderItem)itemQuery!).Amount,
                                                      Amount = ((DO.OrderItem)itemQuery!).Amount
                                                  };
            if (allItems != null)
                boOrder.Items = allItems.ToList();
            boOrder.TotalPrice = boOrder.Items.Sum(o => ((BO.OrderItem)o!).TotalPrice);
            return boOrder;
        }
        /// <summary>
        /// An action that updates the status of the order and returns the updated order
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Order with updated status </returns>
        /// <exception cref="Exception"> If the id is negative an exception is thrown</exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order updateSendingDate(int id)
        {
            if (id < 0)
                throw new BO.NotValidException("ID cannot be negative");
            DO.Order doOrder;
            try
            {
                doOrder = idal!.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("notExist ", ex);
            }
            if (doOrder.ShipDate == null)
            {
                doOrder.ShipDate = DateTime.Now;
                idal.Order.Update(doOrder);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.Status = (BO.OrderStatus)2;
            return newOrder;
        }
        /// <summary>
        /// An action that updates the delivery date and returns the order with an updated delivery date
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns>Returns an order with an updated status </returns>
        /// <exception cref="Exception">If the id is negative an exception is thrown</exception>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order UpdateProvideDate(int id)
        {
            if (id < 0)
                throw new Exception("canot be negative");
            DO.Order doOrder;
            lock (idal!)
            {
                try
                {
                    doOrder = idal!.Order.Get(id);
                }
                catch (Exception ex)
                {
                    throw new BO.NotExistBlException("order not exist", ex);
                }
                if (doOrder.DeliveryDate == null && doOrder.ShipDate != null)
                {
                    doOrder.DeliveryDate = DateTime.Now;
                    idal.Order.Update(doOrder);
                }
                else
                {

                    throw new BO.CannotUpdateDate("cannot update delivery date before ship date");
                }
                BO.Order newOrder = GetOrderDetails(id);

                newOrder.Status = (BO.OrderStatus)3;
                return newOrder;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> order id</param>
        /// <returns> </returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.OrderTracking StatusOrder(int id)
        {
            DO.Order doOrder;
            try
            {
                doOrder = idal!.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("order not exist", ex);
            }
            BO.OrderTracking trackingOrder = new BO.OrderTracking() { ID = doOrder.ID, StatusOrder = CheckStatus(doOrder) };
            trackingOrder.Tracking = new List<Tuple<DateTime?, string?>>();
            Tuple<DateTime?, string?> newTuple = new Tuple<DateTime?, string?>(doOrder.OrderDate, "approved");
            trackingOrder.Tracking.Add(newTuple);
            if (doOrder.ShipDate != null)
            {
                newTuple = new Tuple<DateTime?, string?>(doOrder.ShipDate, "sent");
                trackingOrder.Tracking.Add(newTuple);
            }
            if (doOrder.DeliveryDate != null)
            {
                newTuple = new Tuple<DateTime?, string?>(doOrder.DeliveryDate, "provided");
                trackingOrder.Tracking.Add(newTuple);
            }
            return trackingOrder;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public int? GetOldestOrderId()
        {
            var allOrders =idal!.Order.GetAll(x=>x?.DeliveryDate==null);
           return allOrders.MinBy(x => x?.ShipDate == null ? x?.OrderDate : x?.ShipDate)?.ID;
        }

    }
}
