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
        return DataSource.OrdersList.FirstOrDefault(x=>x?.ID==id)??
        throw new NotExistException(id,"order");     
    }
    /// <summary>
    /// return all the orders in the array
    /// </summary>
    /// <returns>array of orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? check = null)
    {
        if (check != null)
            return from ord in DataSource.OrdersList
                   where check(ord)
                   select ord;
        return from ord in DataSource.OrdersList
               select ord; ;
    }
    /// <summary>
    /// the function delete order from arr
    /// </summary>
    /// <param name="id">id of order</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void Delete(int id)
    {
        int count = DataSource.OrdersList.RemoveAll(ord => ord?.ID == id);
        if (count == 0)
            throw new NotExistException(id, "order");
        //The loop goes through the elements of the array and stops when an product with the same id as the received id is found or until the end of the buffer
        //The loop starts with the element immediately after the order to be deleted and moves each order to the previous position in the array
    }
    /// <summary>
    /// update order
    /// </summary>
    /// <param name="order">order object</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void Update(Order order)
    {
        int count = DataSource.OrdersList.RemoveAll(ord => order.ID == ord?.ID);
        if (count == 0)
            throw new NotExistException(order.ID, "product");

        DataSource.OrdersList.Add(order);
    }
    public Order GetByCondition(Func<Order?, bool>? check)
    {
        return DataSource.OrdersList.Find(x => check!(x)) ??
        throw new DO.NotExistException(1,"order");
    }
    
}
