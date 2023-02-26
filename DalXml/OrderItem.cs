using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Dal;
using DalApi;
using DO;

internal class OrderItem : IOrderItem
{
    const string s_OrderItem = @"OrderItem";
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        if (listOrderItem.RemoveAll(ordItem => ordItem?.ID == id) == 0)
            throw new DO.NotExistException(id, "order item"); 

        XMLTools.SaveListToXMLSerializer(listOrderItem, s_OrderItem);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.OrderItem Get(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        return listOrderItem.FirstOrDefault(ordItem => ordItem?.ID == id) ??
            throw new DO.NotExistException(id, "orderItem");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? check = null)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        if (check == null)
            return listOrderItem.Select(ordItem => ordItem).OrderBy(ordItem => ordItem?.ID);
        else
            return listOrderItem.Where(x=>check(x)).OrderBy(ordItem => ordItem?.ID);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.OrderItem GetByCondition(Func<DO.OrderItem?, bool>? check)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);

        return listOrderItem.FirstOrDefault(x=>check!(x)) ?? throw new DO.NotExistException(0, "ordItem");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public DO.OrderItem GetOrderItemByTwoValues(int product_id, int order_id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        return listOrderItem.FindAll(ordItem => ordItem?.OrderId == order_id && ordItem?.ProductId == product_id).FirstOrDefault()??throw new DO.NotExistException(product_id,"orderItem") ;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update(DO.OrderItem updateObject)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItem);
        if (listOrderItems.RemoveAll(ordItem => ordItem?.ID == updateObject.ID) == 0)
            throw new DO.NotExistException(updateObject.ID, "order");
        listOrderItems.Add(updateObject);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItem);
    }
}
