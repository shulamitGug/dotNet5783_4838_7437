
using DO;
namespace Dal;
using DalApi;
/// <summary>
/// The central department operates on an item in the order
/// </summary>
internal class DalOrderItem:IOrderItem
{
    DalOrder dalo=new DalOrder();
    DalProducts dalp=new DalProducts();
    /// <summary>
    /// The operation adds a member to the item pool in the order
    /// </summary>
    /// <param name="itemOrder">order object</param>
    /// <returns>itemOrder.ID</returns>
    public int Add(OrderItem itemOrder)
    {
        foreach (var item in DataSource.OrderItemList)
        {
            if (item?.ID == itemOrder.ID)
                throw new AlreadyExistException(itemOrder.ID,"orderItem");
        }
        int count = 0;
        double price = (dalp.Get(itemOrder.ProductId)?.Price) ?? 0;
        itemOrder.Price = price;
        foreach (var item in GetOrderItemByOrder(itemOrder.OrderId))
        {
            count++;
        }
        if (count < 4)
        {
            itemOrder.ID = DataSource.Config.GetNextOrderItemNumber();
            //if (DataSource.Config.nextOrderItemIndex >= DataSource.arrOrderItem.Length)
            //    throw new Exception("there is no place");
            DataSource.OrderItemList.Add(itemOrder);
            return itemOrder.ID;
        }
           throw new Exception("there are 4 items in this order");
    }
    /// <summary>
    /// The function returns the item data in the order with the received id
    /// </summary>
    /// <param name="id">id of order item</param>
    /// <returns>all the details of this order item</returns>
    /// <exception cref="Exception"> if the id is not exsist throw an exception</exception>
    public OrderItem? Get(int id)
    {
        //The loop goes through the members of the database and looks for the item in the order with the received id
        foreach (var orderItem in DataSource.OrderItemList)
        {
            if (orderItem?.ID == id)
                return orderItem;
        }
        throw new NotExistException(id,"orderItem");
    }
    /// <summary>
    /// The function returns an array with all the items in the orders
    /// </summary>
    /// <returns> A repository with all the items in the orders</returns>
    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? check = null)
    {
        List<OrderItem> newItemOrders=new List<OrderItem>();
        //We created a new array and the loop copies the items in the orders into the new array
        foreach(DO.OrderItem orderItem in DataSource.OrderItemList)
        {
            if ((check != null && check(orderItem)) || check == null)
                newItemOrders.Add(orderItem);
        }
        return newItemOrders;
    }
    /// <summary>
    /// The operation deletes an item in the order according to the id received
    /// </summary>
    /// <param name="id"> id of order item</param>
    /// <exception cref="Exception">if the id is not exsist throw an exception</exception>
    public void Delete(int id)
    {
        foreach (OrderItem orderItem in DataSource.OrderItemList)
        {
            if (orderItem.ID == id)
            {
                DataSource.OrderItemList.Remove(orderItem);
                return;
            }
        }
        throw new NotExistException(id,"orderItem");
    }
    /// <summary>
    /// the function update order item 
    /// </summary>
    /// <param name="orderItem">order item object </param>
    /// <exception cref="Exception">if the id is not exsist throw an exption</exception>
    public void Update(OrderItem orderItem)
    {
        if (orderItem.Amount != 0 && orderItem.ProductId != 0 && orderItem.OrderId != 0)
        {
            int id, count = 0;
            double price = dalp.Get(orderItem.ProductId)?.Price?? 0;
            orderItem.Price = price;
            //Go through the database until the requested item in the order
            bool isExist = false;
            foreach (var item in GetOrderItemByOrder(orderItem.OrderId))
            {
                count++;
            }
            if (count < 4)
            {
                foreach (OrderItem or in DataSource.OrderItemList)
                {
                    if (or.ID == orderItem.ID)
                    {
                        id = or.OrderId;
                        isExist = true;
                        DataSource.OrderItemList.Remove(or);
                        break;
                    }
                }

                //Go through the database until the requested order
                if (!isExist)
                    throw new NotExistException(orderItem.ID,"orderItem");
                DataSource.OrderItemList.Add(orderItem);
            }
            //else
            //{
            //    throw new DO.fourItemInOrder("xx");
            //}
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
    /// <param name="order_id">id of order</param>
    /// <returns> all the items on this order</returns>
    public IEnumerable<OrderItem?> GetOrderItemByOrder(int order_id)
    {
        List <OrderItem?> newList= new List<OrderItem?>();
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
        foreach (OrderItem or in DataSource.OrderItemList)
        {
            if (check(or))
                return or;
        }
        throw new DO.NotExistException(1, "OrderItem");
    }
}


