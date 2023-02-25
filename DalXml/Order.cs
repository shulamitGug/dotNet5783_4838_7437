using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal;

internal class Order : IOrder
{
    const string s_Order = @"Order";
    public int Add(DO.Order addOrder)
    {
        addOrder.ID = Config.GetNextOrderNumber();
        Config.SaveNextOrderNumber(addOrder.ID + 1);
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        listOrders.Add(addOrder);

        XMLTools.SaveListToXMLSerializer(listOrders, s_Order);

        return addOrder.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (listOrder.RemoveAll(ord => ord?.ID == id) == 0)
            throw new DO.NotExistException(id,"order"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(listOrder, s_Order);
    }

    public DO.Order Get(int id)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        return listOrder.FirstOrDefault(ord => ord?.ID == id) ??
            throw new DO.NotExistException(id,"order");
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? check = null)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);

        if (check == null)
            return listOrder.Select(ord => ord).OrderBy(ord => ord?.ID);
        else
            return listOrder.Where(x=>check(x)).OrderBy(ord => ord?.ID);
    }

    public DO.Order GetByCondition(Func<DO.Order?, bool>? check)
    {
        List<DO.Order?> listOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        return listOrder.FirstOrDefault(x => check!(x)) ?? throw new DO.NotExistException(0, "order");
    }

    public void Update(DO.Order updateObject)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_Order);
        if (listOrders.RemoveAll(ord => ord?.ID == updateObject.ID) == 0)
            throw new DO.NotExistException(updateObject.ID, "order");
        listOrders.Add(updateObject);

        XMLTools.SaveListToXMLSerializer(listOrders, s_Order);
    }
}

