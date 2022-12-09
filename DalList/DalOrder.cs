using DO;
namespace Dal;
using DalApi;
using System.Collections.Generic;
/// <summary>
/// order function
/// </summary>
internal class DalOrder:IOrder
{
    /// <summary>
    /// add order to the array
    /// </summary>
    /// <param name="order">get order object</param>
    /// <returns>if add return the id of this order</returns>
    /// <exception cref="Exception">id there is no place</exception>
    public int Add(Order order)
    {
        foreach(var item in DataSource.OrdersList)
        {
            if (item?.ID == order.ID)
                throw new AlreadyExistException(order.ID,"order");
        }
        order.ID = DataSource.Config.GetNextOrderNumber();
        DataSource.OrdersList.Add(order);
        return order.ID;
    }
    /// <summary>
    /// return all the details of order by order id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>order object</returns>
    /// <exception cref="Exception">if the order is not exist </exception>
    public Order Get(int id)
    {
        //look for the order with the same id
        foreach(Order order in DataSource.OrdersList)
        {
            if(order.ID == id)
                return order;
        }
        throw new NotExistException(id,"order");     
    }
    /// <summary>
    /// return all the orders in the array
    /// </summary>
    /// <returns>array of orders</returns>
    public IEnumerable<Order> GetAll(Func<Order, bool>? check = null)
    {
        List <Order> newOrders = new List<Order>();
        //copy to new arr all the orders that exist the arr
        foreach (Order order in DataSource.OrdersList)
        {
            if ((check != null && check(order)) || check == null)
                newOrders.Add(order);
        }
        return newOrders;
    }
    /// <summary>
    /// the function delete order from arr
    /// </summary>
    /// <param name="id">id of order</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void Delete(int id)
    {
        Order or;
        foreach (Order order in DataSource.OrdersList)
        {
            if (order.ID == id)
            {
                DataSource.OrdersList.Remove(order);
                return;
            }
        }
        //The loop goes through the elements of the array and stops when an product with the same id as the received id is found or until the end of the buffer
        throw new NotExistException(id,"order");
        //The loop starts with the element immediately after the order to be deleted and moves each order to the previous position in the array


    }
    /// <summary>
    /// update order
    /// </summary>
    /// <param name="order">order object</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void Update(Order order)
    {
        bool isExist = false;
            foreach (Order or in DataSource.OrdersList)
            {
                if (or.ID == order.ID)
                {
                    isExist = true;
                    DataSource.OrdersList.Remove(or);
                    break;
                }
            }
        //Go through the database until the requested order
        if (!isExist)
                throw new NotExistException(order.ID,"order");
            DataSource.OrdersList.Add(order);
    }
    public Order GetByCondition(Func<Order, bool>? check)
    {
        foreach (Order or in DataSource.OrdersList)
        {
            if(check(or))
                return or;
        }
        throw new DO.NotExistException(1,"order");
    }

}
