using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Dal;
using DalApi;
using DO;

/// <summary>
/// The central department operates on an item in the order
/// </summary>
 internal class OrderItem : IOrderItem
{
    const string s_OrderItem = @"OrderItem";

    /// <summary>
    /// The operation adds a member to the item pool in the order
    /// </summary>
    /// <param name="itemOrder">order object</param>
    /// <returns>itemOrder.ID</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem addObject)
    {
        List<DO.OrderItem?> listitems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        addObject.ID = Config.GetNextOrderItemNumber();
        Config.SaveNextOrderItemNumber(addObject.ID + 1);
        listitems.Add(addObject);

        XMLTools.SaveListToXMLSerializer(listitems, s_OrderItem);

        return addObject.ID;
    }

    /// <summary>
    /// The operation deletes an item in the order according to the id received
    /// </summary>
    /// <param name="id"> id of order item</param>
    /// <exception cref="Exception">if the id is not exsist throw an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        if (listOrderItem.RemoveAll(ordItem => ordItem?.ID == id) == 0)
            throw new DO.NotExistException(id, "order item"); 

        XMLTools.SaveListToXMLSerializer(listOrderItem, s_OrderItem);
    }

    /// <summary>
    /// The function returns the item data in the order with the received id
    /// </summary>
    /// <param name="id">id of order item</param>
    /// <returns>all the details of this order item</returns>
    /// <exception cref="Exception"> if the id is not exsist throw an exception</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        return listOrderItem.FirstOrDefault(ordItem => ordItem?.ID == id) ??
            throw new DO.NotExistException(id, "orderItem");
    }


    /// <summary>
    /// The function returns an array with all the items in the orders
    /// </summary>
    /// <returns> A repository with all the items in the orders</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? check = null)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        if (check == null)
            return listOrderItem.Select(ordItem => ordItem).OrderBy(ordItem => ordItem?.ID);
        else
            return listOrderItem.Where(x=>check(x)).OrderBy(ordItem => ordItem?.ID);
    }



    /// <summary>
    /// The function returns all the items in the order according to the received id
    /// </summary>
    /// <param name = "order_id" > id of order</param>
    /// <returns> all the items on this order</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem GetByCondition(Func<DO.OrderItem?, bool>? check)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        return listOrderItem.FirstOrDefault(x=>check!(x)) ?? throw new DO.NotExistException(0, "ordItem");
    }

    /// <summary>
    /// The function returns the item in the order that is adjusted to the received values
    /// </summary>
    /// <param name="product_id"> id of product</param>
    /// <param name="order_id">id of order</param>
    /// <returns> item on order</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem GetOrderItemByTwoValues(int product_id, int order_id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        return listOrderItem.FindAll(ordItem => ordItem?.OrderId == order_id && ordItem?.ProductId == product_id).FirstOrDefault()??throw new DO.NotExistException(product_id,"orderItem") ;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]


    /// <summary>
    /// the function update order item 
    /// </summary>
    /// <param name="orderItem">order item object </param>
    /// <exception cref="Exception">if the id is not exsist throw an exption</exception>
    public void Update(DO.OrderItem updateObject)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        if (listOrderItems.RemoveAll(ordItem => ordItem?.ID == updateObject.ID) == 0)
            throw new DO.NotExistException(updateObject.ID, "order");
        listOrderItems.Add(updateObject);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItem);
    }
}
