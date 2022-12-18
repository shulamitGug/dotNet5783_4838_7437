using DO;
public class Program
{
    /// <summary>
    /// A function for all the actions you want to do on orders
    /// </summary>
    static DalApi.IDal? idal = DalApi.Factory.Get();
    public static void OrderFunction()
    {
        int num2;
        Console.WriteLine("insert number 0 to exit,1 to add order,2 to get order by id,3 to get all orders,4 to delete order and 5 to update order");
        num2 = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
       
        //Checking the number and sending it to the appropriate action
        switch (num2)
        {
            case 0:
                break;
            
            case 1:
                {
                    //data reception
                    Console.WriteLine("insert customer name,adress,email,order date,ship date,delivery date");
                    string name = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string adress = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string email = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string ordDate = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string shipDate = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string deliveryDate = Console.ReadLine() ?? throw new Exception("cannot be empty");
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
                        Console.WriteLine(idal.Order.Add(o));
                }
                break;
            
            case 2:
                {
                    //get by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id=int.Parse(Console.ReadLine()??throw new Exception("cannot be empty"));
                    Order o = new Order();
                    o = idal.Order.Get(id);
                    Console.WriteLine(o);
                }
                break;
            
            case 3:
                {
                    //get all and print
                    IEnumerable<Order?> orders = idal.Order.GetAll();
                    foreach (var item in orders)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine();
                    }
                }
                break;
            
            case 4:
                {
                    //update
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Order o = new Order();
                    o = idal.Order.Get(id);
                    Console.WriteLine(o);
                    Console.WriteLine("insert customer name,adress,email,orderDate,shipDate,deliverydate");
                    string name = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string adress = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string email = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string ordDate= Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string shipDate= Console.ReadLine() ?? throw new Exception("cannot be empty");
                    string deliveryDate= Console.ReadLine() ?? throw new Exception("cannot be empty");
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
                    idal?.Order.Update(newO);                }
                break;
            
            case 5:
                {
                    //delete order
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine()??throw new Exception("cannot be empty"));
                    idal?.Order.Delete(id);
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
        num2 = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        //Checking the number and sending it to the appropriate action

        switch (num2)
        {
            case 0:
                break;
            
            case 1:
                {
                    Console.WriteLine("insert product id, product name,price,in stock and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories");
                    //data reception
                    int id=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));  
                    string name = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    int price=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int inStock=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int category=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    //Creating a new object with the absorbed donors
                    Product p = new Product()
                    {
                        ID=id,
                        Name = name,
                        CategoryP = (Category)category,
                        Price = price,
                        InStock =inStock,
                    };
                    Console.WriteLine(idal.Product.Add(p));

                }
                break;
            
            case 2:
                {
                    //get product by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Product p = new Product();
                    p = idal.Product.Get(id);
                    Console.WriteLine(p);
                }
                break;
            
            case 3:
                {
                    //get all products and print
                    IEnumerable<Product?> products = idal.Product.GetAll();
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine();
                    }
                }
                break;
            
            case 4:
                {
                    //update product
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Product p = new Product();
                    p = idal.Product.Get(id);
                    Console.WriteLine(p);
                    Console.WriteLine("insert product name,price,in stock and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories");
                    string name = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    int price = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int inStock = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int category=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                        //Creating a new object with the absorbed donors
                        Product newp = new Product()
                        {
                            ID = id,
                            Name = name,
                            CategoryP = (Category)category,
                            Price = price,
                            InStock = inStock,
                        };
                    idal.Product.Update(newp);
                }
                break;
            
            case 5:
                {
                    //delete product
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    idal.Product.Delete(id);
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
        num2 = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        //Checking the number and sending it to the appropriate action
        switch (num2)
        {
            case 0:
                break;
            
            case 1:
                {
                    Console.WriteLine("inser amount, productId,orderId");
                    //data reception
                    int amount = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int product_id=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int order_id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    //Creating a new object with the absorbed donors
                    OrderItem oi = new OrderItem()
                    {
                        ProductId = product_id,
                        Amount = amount,
                        OrderId = order_id,
                        
                    };
                    Console.WriteLine(idal.OrderItem.Add(oi));

                }
                break;
            
            case 2:
                {
                    //get item in order by id and print
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    OrderItem oi = new OrderItem();
                    oi= idal.OrderItem.Get(id);
                    Console.WriteLine(oi);
                }
                break;
           
            case 3:
                {
                    //get all items in orders and print
                   IEnumerable<OrderItem?> ordersItem = idal.OrderItem.GetAll();
                   foreach(var item in ordersItem)       
                    {
                        Console.WriteLine(item);
                        Console.WriteLine();
                    }
                }
                break;
            
            case 4:
                {
                    //update
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    OrderItem oi = new OrderItem();
                    oi = idal.OrderItem.Get(id);
                    Console.WriteLine(oi);
                    Console.WriteLine("insert product id,order id and amount");
                    int prod_id=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int ord_id=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int amount = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    double price = idal.Product.Get(prod_id).Price;
                    //Creating a new object with the absorbed donors
                    OrderItem newoi = new OrderItem()
                        {
                            ID = id,
                            ProductId=prod_id,
                            OrderId=ord_id,
                            Amount = amount,
                            Price = price
                        };
                    idal.OrderItem.Update(newoi);
                }
                break;
            
            case 5:
                {
                    //delete
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    idal.OrderItem.Delete(id);
                }
                break;
            
            case 6:
                {
                    //get by order id and product id
                    Console.WriteLine("enter product id,order id");
                    int prod=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int ord=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(idal.OrderItem.GetOrderItemByTwoValues(prod,ord));
                }
                break;
            
            case 7:
                {
                    //get by order id
                    Console.WriteLine("enter order id");
                    int ord = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    IEnumerable<OrderItem?> itemList = idal.OrderItem.GetAll(x=>x?.OrderId==ord);
                    foreach (var item in itemList)
                    {
                        Console.WriteLine(item);
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
        num = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
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
                num = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
            }
        }
        catch (Exception ex)
        { Console.WriteLine(ex.Message); }

        
    }

}
