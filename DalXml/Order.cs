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
    public void Delete(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (listOrder.RemoveAll(ord => ord?.ID == id) == 0)
            throw new DO.NotExistException(id,"order"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(listOrder, s_Order);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        return listOrder.FirstOrDefault(ord => ord?.ID == id) ??
            throw new DO.NotExistException(id,"order");
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? check = null)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (check == null)
            return listOrder.Select(ord => ord).OrderBy(ord => ord?.ID);
        else
            return listOrder.Where(x=>check(x)).OrderBy(ord => ord?.ID);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order GetByCondition(Func<DO.Order?, bool>? check)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        return listOrder.FirstOrDefault(x => check!(x)) ?? throw new DO.NotExistException(0, "order");
    }
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

