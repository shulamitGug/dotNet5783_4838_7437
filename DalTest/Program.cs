using DO;
using Dal;

public class Program
{
    /// <summary>
    /// A function for all the actions you want to do on orders
    /// </summary>
    public static void OrderFunction()
    {
        int num2;
        Console.WriteLine("insert number 0 to exit,1 to add order,2 to get order by id,3 to get all orders,4 to delete order and 5 to update order");
        num2 = int.Parse(Console.ReadLine());
        DalOrder dalO = new DalOrder();
        //Checking the number and sending it to the appropriate action
        switch (num2)
        {
            case 0:
                break;
            case 1:
                {
                    //data reception
                    Console.WriteLine("insert customer name,adress,email,order date,ship date,delivery date");
                    string name = Console.ReadLine();
                    string adress = Console.ReadLine();
                    string email = Console.ReadLine();
                    string ordDate = Console.ReadLine();
                    string shipDate = Console.ReadLine();
                    string deliveryDate = Console.ReadLine();
                    DateTime.TryParse(ordDate, out DateTime dtOrder);
                    DateTime.TryParse(shipDate, out DateTime dtShip);
                    DateTime.TryParse(deliveryDate, out DateTime dtDelivery);
                    //Creating a new object with the absorbed donors
                    Order o = new Order()
                        {
                            CustomerName = name,
                            CustomerAdress = adress,
                            CustomerEmail = email,
                            OrderDate = dtOrder,
                            ShipDate = dtShip,
                            DeliveryDate = dtDelivery,
                        };
                        Console.WriteLine(dalO.AddOrder(o));
                }
                break;
            case 2:
                {
                    //get by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id=int.Parse(Console.ReadLine());
                    Order o = new Order();
                    o = dalO.GetOrder(id);
                    Console.WriteLine(o);
                }
                break;
            case 3:
                {
                    //get all and print
                    Order[] orders = dalO.GetAllOrders();
                    for(int i = 0; i < orders.Length; i++)
                    {
                        Console.WriteLine(orders[i]);
                        Console.WriteLine();
                    }
                }
                break;
            case 4:
                {
                    //update
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    Order o = new Order();
                    o = dalO.GetOrder(id);
                    Console.WriteLine(o);
                    Console.WriteLine("insert customer name,adress,email,orderDate,shipDate,deliverydate");
                    string name = Console.ReadLine();
                    string adress = Console.ReadLine();
                    string email = Console.ReadLine();
                    string ordDate= Console.ReadLine();
                    string shipDate= Console.ReadLine();
                    string deliveryDate= Console.ReadLine();
                    DateTime.TryParse(ordDate, out DateTime dtOrder);
                    DateTime.TryParse(shipDate, out DateTime dtShip);
                    DateTime.TryParse(deliveryDate, out DateTime dtDelivery);
                        Order newO = new Order()
                        {
                            ID = id,
                            CustomerName = name,
                            CustomerAdress = adress,
                            CustomerEmail = email,
                            OrderDate = dtOrder,
                            ShipDate = dtShip,
                            DeliveryDate = dtDelivery,
                        };
                        dalO.UpdateOrder(newO);                }
                break;
            case 5:
                {
                    //delete order
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    dalO.DeleteOrder(id);
                }
                break;
        }
    }
    /// <summary>
    /// A function for all the actions you want to do on orders
    /// </summary>
    public static void ProductFunction()
    {
        int num2;
        Console.WriteLine("insert number 0 to exit,1 to add product,2 to get product by id,3 to get all product,4 to delete product and 5 to update product");
        num2 = int.Parse(Console.ReadLine());
        DalProducts dalp = new DalProducts();
        //Checking the number and sending it to the appropriate action

        switch (num2)
        {
            case 0:
                break;
            case 1:
                {
                    Console.WriteLine("insert product name,price,in stock and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories");
                    //data reception
                    string name = Console.ReadLine();
                    int price=int.Parse(Console.ReadLine());
                    int inStock=int.Parse(Console.ReadLine());
                    int category=int.Parse(Console.ReadLine());
                    //Creating a new object with the absorbed donors
                    Products p = new Products()
                    {
                        Name = name,
                        CategoryP = (Category)category,
                        Price = price,
                        InStock =inStock,
                    };
                    Console.WriteLine(dalp.AddProduct(p));

                }
                break;
            case 2:
                {
                    //get product by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    Products p = new Products();
                    p = dalp.GetProduct(id);
                    Console.WriteLine(p);
                }
                break;
            case 3:
                {
                    //get all products and print
                    Products[] products = dalp.GetAllProduct();
                    for (int i = 0; i < products.Length; i++)
                    {
                        Console.WriteLine(products[i]);
                        Console.WriteLine();
                    }
                }
                break;
            case 4:
                {
                    //update product
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    Products p = new Products();
                    p = dalp.GetProduct(id);
                    Console.WriteLine(p);
                    Console.WriteLine("insert product name,price,in stock and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories");
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int inStock = int.Parse(Console.ReadLine());
                    int category=int.Parse(Console.ReadLine());
                    if (name != "" && price != 0 && inStock != 0 && category != 0)
                    {
                        //Creating a new object with the absorbed donors
                        Products newp = new Products()
                        {
                            ID = id,
                            Name = name,
                            CategoryP = (Category)category,
                            Price = price,
                            InStock = inStock,
                        };
                        dalp.UpdateProduct(newp);
                    }
                }
                break;
            case 5:
                {
                    //delete product
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    dalp.DeleteProduct(id);
                }
                break;
        }
    }
    /// <summary>
    /// A function for all the actions you want to do on orders
    /// </summary>
    public static void OrderItemFunction()
    {
        int num2;
        Console.WriteLine("insert number 0 to exit,1 to add orderItem,2 to get orderItem by id,3 to get all orderItem,4 to delete orderItem and 5 to update orderItem,6 to get orderItem by order id and product id and 7 to get all the items in order by order id");
        num2 = int.Parse(Console.ReadLine());
        DalOrderItem daloi = new DalOrderItem();
        DalProducts dalp = new DalProducts();
        //Checking the number and sending it to the appropriate action
        switch (num2)
        {
            case 0:
                break;
            case 1:
                {
                    Console.WriteLine("inser amount, productId,orderId");
                    //data reception
                    int amount = int.Parse(Console.ReadLine());
                    int product_id=int.Parse(Console.ReadLine());
                    int order_id = int.Parse(Console.ReadLine());
                    double price = dalp.GetProduct(product_id).Price;
                    //Creating a new object with the absorbed donors
                    OrderItem oi = new OrderItem()
                    {
                        ProductId = product_id,
                        Amount = amount,
                        OrderId = order_id,
                        Price = price,
                    };
                    Console.WriteLine(daloi.AddOrderItem(oi));

                }
                break;
            case 2:
                {
                    //get item in order by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    OrderItem oi = new OrderItem();
                    oi= daloi.GetOrderItem(id);
                    Console.WriteLine(oi);
                }
                break;
            case 3:
                {
                    //get all items in orders and print
                    OrderItem[] ordersItem = daloi.GetAllOrderItem();
                    for (int i = 0; i < ordersItem.Length; i++)
                    {
                        Console.WriteLine(ordersItem[i]);
                        Console.WriteLine();
                    }
                }
                break;
            case 4:
                {
                    //update
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    OrderItem oi = new OrderItem();
                    oi = daloi.GetOrderItem(id);
                    Console.WriteLine(oi);
                    Console.WriteLine("insert product id,order id and amount");
                    int prod_id=int.Parse(Console.ReadLine());
                    int ord_id=int.Parse(Console.ReadLine());
                    int amount = int.Parse(Console.ReadLine());
                    double price = dalp.GetProduct(prod_id).Price;
                    //Creating a new object with the absorbed donors
                    OrderItem newoi = new OrderItem()
                        {
                            ID = id,
                            ProductId=prod_id,
                            OrderId=ord_id,
                            Amount = amount,
                            Price = price
                        };
                        daloi.UpdateOrderItem(newoi);
                }
                break;
            case 5:
                {
                    //delete
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    daloi.DeleteOrderItem(id);
                }
                break;
            case 6:
                {
                    //get by order id and product id
                    Console.WriteLine("enter product id,order id");
                    int prod=int.Parse(Console.ReadLine());
                    int ord=int.Parse(Console.ReadLine());
                    Console.WriteLine(daloi.GetOrderItemByTwoValues(prod,ord));
                }
                break;
            case 7:
                {
                    //get by order id
                    Console.WriteLine("enter order id");
                    int ord = int.Parse(Console.ReadLine());
                    OrderItem[] arr = daloi.GetOrderItemByOrder(ord);
                    for(int i=0; i < arr.Length; i++)
                    {
                        Console.WriteLine(arr[i]);
                        Console.WriteLine();
                    }
                    break;
                }
        }
    }
    public static void Main()
    {
        
        int num;
        Console.WriteLine("insert 1 to product,2 to order and 3 to orderItem and 0 to stop");
        num = int.Parse(Console.ReadLine());
        try
        {
            while (num != 0)
            {
                if (num == 1)//product
                    ProductFunction();
                else if (num == 2)//order
                    OrderFunction();
                else if (num == 3)//order item
                    OrderItemFunction();
                Console.WriteLine("insert 1 to product,2 to order and 3 to orderItem and 0 to stop");
                num = int.Parse(Console.ReadLine());
            }
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }

        
    }

}
