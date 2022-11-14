
using DO;
namespace Dal;
/// <summary>
/// The central department operates on an item in the order
/// </summary>
public class DalOrderItem
{
    /// <summary>
    /// The operation adds a member to the item pool in the order
    /// </summary>
    /// <param name="itemOrder">order object</param>
    /// <returns>itemOrder.ID</returns>
    public int AddOrderItem(OrderItem itemOrder)
    {
        itemOrder.ID = DataSource.Config.GetNextOrderItemNumber();
        if (DataSource.Config.nextOrderItemIndex >= DataSource.arrOrderItem.Length)
            throw new Exception("there is no place");
        DataSource.arrOrderItem[DataSource.Config.nextOrderItemIndex++] = itemOrder;
        return itemOrder.ID;
    }
    /// <summary>
    /// The function returns the item data in the order with the received id
    /// </summary>
    /// <param name="id">id of order item</param>
    /// <returns>all the details of this order item</returns>
    /// <exception cref="Exception"> if the id is not exsist throw an exception</exception>
    public OrderItem GetOrderItem(int id)
    {
        //The loop goes through the members of the database and looks for the item in the order with the received id
        for (int i = 0; i < DataSource.arrOrderItem.Length; i++)
        {
            if (DataSource.arrOrderItem[i].ID == id)
                return DataSource.arrOrderItem[i];
        }
        throw new Exception("the OrderItem is not exist");
    }
    /// <summary>
    /// The function returns an array with all the items in the orders
    /// </summary>
    /// <returns> A repository with all the items in the orders</returns>
    public OrderItem[] GetAllOrderItem()
    {
        OrderItem[] items = DataSource.arrOrderItem;
        OrderItem[] newItemOrders = new OrderItem[DataSource.Config.nextOrderItemIndex];
        //We created a new array and the loop copies the items in the orders into the new array
        for (int i = 0; i < DataSource.Config.nextOrderItemIndex; i++)
        {
            newItemOrders[i] = items[i];
        }
        return newItemOrders;
    }
    /// <summary>
    /// The operation deletes an item in the order according to the id received
    /// </summary>
    /// <param name="id"> id of order item</param>
    /// <exception cref="Exception">if the id is not exsist throw an exception</exception>
    public void DeleteOrderItem(int id)
    {
        int i;
        //The loop goes through the elements of the array and stops when an item with the same id as the received id is found or until the end of the buffer
        for (i = 0; i < DataSource.arrOrderItem.Length && DataSource.arrOrderItem[i].ID != id; i++) ;
        if (i >= DataSource.arrOrderItem.Length) 
            throw new Exception("the OrderItem is not exist");
        //The loop starts with the element immediately after the item to be deleted and moves each item to the previous position in the array
        for (int j = i + 1; j < DataSource.arrOrderItem.Length; j++)
        {
            DataSource.arrOrderItem[j - 1] = DataSource.arrOrderItem[j];
        }
        DataSource.Config.nextOrderItemIndex--;
    }
    /// <summary>
    /// the function update order item 
    /// </summary>
    /// <param name="orderItem">order item object </param>
    /// <exception cref="Exception">if the id is not exsist throw an exption</exception>
    public void UpdateOrderItem(OrderItem orderItem)
    {
        if (orderItem.Amount != 0 && orderItem.ProductId != 0 && orderItem.OrderId != 0)
        {
            int i;
            //Go through the database until the requested item in the order
            for (i = 0; i < DataSource.arrOrderItem.Length && DataSource.arrOrderItem[i].ID != orderItem.ID; i++) ;
            if (i >= DataSource.arrOrderItem.Length)
                throw new Exception("the orderItem is not exist");
            DataSource.arrOrderItem[i] = orderItem;
        }
    }
    /// <summary>
    /// The function returns the item in the order that is adjusted to the received values
    /// </summary>
    /// <param name="product_id"> id of product</param>
    /// <param name="order_id">id of order</param>
    /// <returns> item on order</returns>
    public OrderItem GetOrderItemByTwoValues(int product_id,int order_id)
    {
        //Go over the item pool
        for (int i=0; i < DataSource.arrOrderItem.Length; i++)
        {
            if (DataSource.arrOrderItem[i].OrderId == order_id&& DataSource.arrOrderItem[i].ProductId ==product_id)
                return DataSource.arrOrderItem[i];
        }

        return new OrderItem();
    }
    /// <summary>
    /// The function returns all the items in the order according to the received id
    /// </summary>
    /// <param name="order_id">id of order</param>
    /// <returns> all the items on this order</returns>
    public OrderItem[] GetOrderItemByOrder(int order_id)
    {
        OrderItem[] arr=new OrderItem[DataSource.arrOrderItem.Length];
        int j = 0;
        //During the loop, all the items in the received order are filled
        for (int i = 0; i < DataSource.arrOrderItem.Length; i++)
        {
            if (DataSource.arrOrderItem[i].OrderId == order_id)
                 arr[j++] = DataSource.arrOrderItem[i];
        }
        OrderItem[] newArr = new OrderItem[j];
        //The loop goes through the array of items and shrinks spaces
        for (int i = 0; i < newArr.Length; i++)
        {
            newArr[i]=arr[i];
        }

        return newArr;
    }

}
