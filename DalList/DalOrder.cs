using DO;
namespace Dal;
/// <summary>
/// order function
/// </summary>
public class DalOrder
{
    Order[] order = DataSource.arrOrders;
    /// <summary>
    /// add order to the array
    /// </summary>
    /// <param name="order">get order object</param>
    /// <returns>if add return the id of this order</returns>
    /// <exception cref="Exception">id there is no place</exception>
    public int AddOrder(Order order)
    {
        order.ID = DataSource.Config.GetNextOrderNumber();
        if (order.OrderDate > order.ShipDate|| order.ShipDate>order.DeliveryDate)
            throw new Exception("not valid dates");
        if (DataSource.Config.nextOrderIndex >= DataSource.arrOrders.Length)
            throw new Exception("there is no place");
        DataSource.arrOrders[DataSource.Config.nextOrderIndex++] = order;
        return order.ID;
    }
    /// <summary>
    /// return all the details of order by order id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>order object</returns>
    /// <exception cref="Exception">if the order is not exist </exception>
    public Order GetOrder(int id)

    {
        //look for the order with the same id
        for(int i=0;i<DataSource.arrOrders.Length;i++)
        {
            if(DataSource.arrOrders[i].ID == id)
                return DataSource.arrOrders[i];
        }
        throw new Exception("the order is not exist");     
    }
    /// <summary>
    /// return all the orders in the array
    /// </summary>
    /// <returns>array of orders</returns>
    public Order[] GetAllOrders()
    {
        Order [] orders = DataSource.arrOrders;
        Order[] newOrders = new Order[DataSource.Config.nextOrderIndex];  
        //copy to new arr all the orders that exist the arr
        for (int i=0;i< DataSource.Config.nextOrderIndex; i++)
        {
            newOrders[i]=orders[i];
        }
        return newOrders;
    }
    /// <summary>
    /// the function delete order from arr
    /// </summary>
    /// <param name="id">id of order</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void DeleteOrder(int id)

    {
        int i;
        //The loop goes through the elements of the array and stops when an product with the same id as the received id is found or until the end of the buffer
        for ( i = 0; i <= DataSource.arrOrders.Length && DataSource.arrOrders[i].ID != id; i++) ;
        if(i> DataSource.arrOrders.Length)
        throw new Exception("the order is not exist");
        //The loop starts with the element immediately after the order to be deleted and moves each order to the previous position in the array
        for (int j=i+1;j < DataSource.arrOrders.Length;j++)
        {
            DataSource.arrOrders[j-1] = DataSource.arrOrders[j];
        }
        DataSource.Config.nextOrderIndex--;

    }
    /// <summary>
    /// update order
    /// </summary>
    /// <param name="order">order object</param>
    /// <exception cref="Exception">the order is not exist</exception>
    public void UpdateOrder(Order order)
    {
        if (order.CustomerName != "" && order.CustomerAdress != "" && order.CustomerEmail != "")
        {
            if (order.OrderDate > order.ShipDate || order.ShipDate > order.DeliveryDate)
                throw new Exception("not valid dates");
            int i;
            //Go through the database until the requested order
            for (i = 0; i < DataSource.arrOrders.Length && DataSource.arrOrders[i].ID != order.ID; i++) ;
            if (i >= DataSource.arrOrders.Length)
                throw new Exception("the order is not exist");
            DataSource.arrOrders[i] = order;
        }
       
    }

}
