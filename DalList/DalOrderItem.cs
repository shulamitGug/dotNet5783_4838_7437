
using DO;
namespace Dal;
using DalApi;
/// <summary>
/// The central department operates on an item in the order
/// </summary>
internal class DalOrderItem:IOrderItem
{
    /// <summary>
    /// The operation adds a member to the item pool in the order
    /// </summary>
    /// <param name="itemOrder">order object</param>
    /// <returns>itemOrder.ID</returns>
    public int Add(OrderItem itemOrder)
    {

        int count = 0;
        double price = DataSource.ProductsList.FirstOrDefault(x=>x?.ID== itemOrder.ProductId)?.Price??throw new DO.NotExistException(itemOrder.ProductId,"product");
        itemOrder.Price = price;
            itemOrder.ID = DataSource.Config.GetNextOrderItemNumber();
            DataSource.OrderItemList.Add(itemOrder);
            return itemOrder.ID;

    }
    /// <summary>
    /// The function returns the item data in the order with the received id
    /// </summary>
    /// <param name="id">id of order item</param>
    /// <returns>all the details of this order item</returns>
    /// <exception cref="Exception"> if the id is not exsist throw an exception</exception>
    public OrderItem Get(int id)
    {
        //The loop goes through the members of the database and looks for the item in the order with the received id

        return DataSource.OrderItemList.FirstOrDefault(x => x?.ID == id)??
        throw new NotExistException(id,"orderItem");
    }
    /// <summary>
    /// The function returns an array with all the items in the orders
    /// </summary>
    /// <returns> A repository with all the items in the orders</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? check = null)
    {
        if (check != null)
            return from ordItem in DataSource.OrderItemList
                   where check(ordItem)
                   select ordItem;
        return from ordItem in DataSource.OrderItemList
               select ordItem; ;
    }
    /// <summary>
    /// The operation deletes an item in the order according to the id received
    /// </summary>
    /// <param name="id"> id of order item</param>
    /// <exception cref="Exception">if the id is not exsist throw an exception</exception>
    public void Delete(int id)
    {
        int count = DataSource.OrderItemList.RemoveAll(ordItem => ordItem?.ID == id);
        if (count == 0)
            throw new NotExistException(id, "orderItem");
    }
    /// <summary>
    /// the function update order item 
    /// </summary>
    /// <param name="orderItem">order item object </param>
    /// <exception cref="Exception">if the id is not exsist throw an exption</exception>
    public void Update(OrderItem orderItem)
    {
        int count = DataSource.OrderItemList.RemoveAll(ordItem => orderItem.ID == ordItem?.ID);
        if (count == 0)
            throw new NotExistException(orderItem.ID, "product");
        DataSource.OrderItemList.Add(orderItem);
        
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
    foreach (OrderItem or in DataSource.OrderItemList)
    {
            if (or.OrderId == order_id&& or.ProductId ==product_id)
                return or;
    }

        throw new NotExistException(product_id,$"product in order number {order_id}");
    }

    /// <summary>
    /// The function returns all the items in the order according to the received id
    /// </summary>
    /// <param name = "order_id" > id of order</param>
    /// <returns> all the items on this order</returns>
    public IEnumerable<OrderItem?> GetOrderItemByOrder(int order_id)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        //During the loop, all the items in the received order are filled
        foreach (OrderItem or in DataSource.OrderItemList)
        {
            if (or.OrderId == order_id)
                newList.Add(or);
        }

        return newList;
    }
    public OrderItem GetByCondition(Func<OrderItem?, bool>? check)
    {
        return DataSource.OrderItemList.Find(x => check(x)) ??
        throw new DO.NotExistException(1, "OrderItem");
    }
}


