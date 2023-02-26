using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace Dal;

internal class Order : IOrder
{
    const string s_Order = @"Order";    
    [MethodImpl(MethodImplOptions.Synchronized)]
    
    /// <summary>
    /// add order to the array
    /// </summary>
    /// <param name="order">get order object</param>
    /// <returns>if add return the id of this order</returns>
    /// <exception cref="Exception">id there is no place</exception>
    public int Add(DO.Order addOrder)
    {
        addOrder.ID = Config.GetNextOrderNumber();
        Config.SaveNextOrderNumber(addOrder.ID + 1);
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        listOrders.Add(addOrder);

        XMLTools.SaveListToXMLSerializer(listOrders, s_Order);

        return addOrder.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]


    /// <summary>
    /// the function delete order from arr
    /// </summary>
    /// <param name="id">id of order</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void Delete(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (listOrder.RemoveAll(ord => ord?.ID == id) == 0)
            throw new DO.NotExistException(id,"order"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(listOrder, s_Order);
    }



    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// return all the details of order by order id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>order object</returns>
    /// <exception cref="Exception">if the order is not exist </exception>
    public DO.Order Get(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        return listOrder.FirstOrDefault(ord => ord?.ID == id) ??
            throw new DO.NotExistException(id,"order");
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    /// <summary>
    /// return all the orders in the array
    /// </summary>
    /// <returns>array of orders</returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? check = null)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (check == null)
            return listOrder.Select(ord => ord).OrderBy(ord => ord?.ID);
        else
            return listOrder.Where(x=>check(x)).OrderBy(ord => ord?.ID);
    }


    /// <summary>
    /// get by condition
    /// </summary>
    /// <param name="check"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order GetByCondition(Func<DO.Order?, bool>? check)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        return listOrder.FirstOrDefault(x => check!(x)) ?? throw new DO.NotExistException(0, "order");
    }


    /// <summary>
    /// update order
    /// </summary>
    /// <param name="order">order object</param>
    /// <exception cref="Exception">the order is not exist</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order updateObject)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        if (listOrders.RemoveAll(ord => ord?.ID == updateObject.ID) == 0)
            throw new DO.NotExistException(updateObject.ID, "order");
        listOrders.Add(updateObject);

        XMLTools.SaveListToXMLSerializer(listOrders, s_Order);
    }
}

