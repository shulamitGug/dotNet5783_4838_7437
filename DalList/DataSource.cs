using DO;
namespace Dal;
using System.Collections.Generic;
/// <summary>
/// all the data of the store
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// Internal class for automatic id calculation and also for index calculation
    /// </summary>
    internal static class Config
    {
        /// <summary>
        /// id in automatic numbering
        /// </summary>
        private static int nextOrderNumber = 0;
        private static int nextOrderItemNumber = 0;

        /// <summary>
        /// return the next id
        /// </summary>
        /// <returns>Next Order id</returns>
        public static int GetNextOrderNumber()
        {
            nextOrderNumber++;
            return nextOrderNumber;
        }
        public static int GetNextOrderItemNumber()
        {
            nextOrderItemNumber++;
            return nextOrderItemNumber;
        }
    }
    //random
    private static readonly Random s_rand = new();
    //data storage arrays
    internal static List<Order?> OrdersList = new List<Order?>();
    internal static List<Product?> ProductsList = new List<Product?>();
    internal static List<OrderItem?> OrderItemList = new List<OrderItem?>();
    
     /// <summary>
     /// constructor
     /// </summary>
    static DataSource()
    {
        S_Initialize();
    }
    /// <summary>
    /// Initializing an order pool with little data
    /// </summary>
    private static void CreateInitilaizeOrder()
    {
        string[] arrCustomerName = { "Daniel kirshenboim", "Shalom Levi", "David Cohen", "Yosi Wis", "Shimi Segal", "Israel Lubin", "Moshe Sason", "Yakov Gros", "Menashe Grin", "Yehuda Cazt" };
        string[] arrCustomerAdress = { "Gotlib 2", "ovadia 1", "hanasi 3", "rabbi akiva 23", "ahronovith 8", "Gordon 4", "Pinkas 77", "Dakar 2", "Tarfon 3", "Golomb 4" };
        string[] arrCustomerEmail = { "s@gmail.com", "m@gmail.com", "n@gmail.com", "t@gmail.com", "y@gmail.com", "a@gmail.com", "d@gmail.com", "w@gmail.com", "q@gmail.com", "p@gmail.com" };

        for (int i = 0; i < 10; i++)
        {
                TimeSpan ts = new TimeSpan(s_rand.Next(5));
                TimeSpan ts1 = new TimeSpan(s_rand.Next(200));
            DateTime? d,t;
            if (i % 2 == 0)
            {
                 d = null;
                t = null;
            }

            else
            {
                 d = DateTime.MinValue + ts;
                t = DateTime.MinValue + ts + ts1;
            }

            Order o = new Order()
            {
                ID = Config.GetNextOrderNumber(),
                CustomerName = arrCustomerName[i],
                CustomerAdress = arrCustomerAdress[i],
                CustomerEmail = arrCustomerEmail[s_rand.Next(arrCustomerName.Length)],
                OrderDate = DateTime.Today,
                ShipDate = d,
                DeliveryDate = t,
            };
            OrdersList.Add(o);

        }
    }
    /// <summary>
    /// Initializing an product pool with little data
    /// </summary>
    private static void CreateInitilaizeProduct()
    {
        int cat;
        string[] arrProductName = { "Makup", "blush", "Primer", "silhouettes", "Rimmel", "Shimmer", "eye brush", "Brush blush", "Eyeliner", "Concealer", "lipstick", "powder", "Bronzer", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush" };
        
        for (int i = 0; i < 20; i++)
        {
            int instock;
            if (i % 4 == 0)
                instock = 0;
            else
                instock = s_rand.Next(10) + 200;
            if (i < 6)
                cat = i+1;
            else if((i + 1) % 6 == 0)
                cat = (i+2) % 6;
            else
                cat = (i + 1) % 6;
            Product p = new Product()
            {
                ID = i+1000000,
                Name = arrProductName[i],
                CategoryP = (Category)(cat),
                Price = s_rand.Next(10) + 450,
                InStock =instock,
            };
            ProductsList.Add (p);
        }
    }
    /// <summary>
    /// Initializing an order item pool with little data
    /// </summary>
    private static void CreateInitilaizeOrderItem()
    {
        double price=0;
        for (int i = 0; i < 40; i++)
        {
            int product = s_rand.Next(20)+1000000;
            foreach (var item in ProductsList)
            {
                if(item?.ID==product)
                    price = item?.Price??0;
            }
            if (price == 0)
                throw new Exception("the product is not exist");
            OrderItem oi = new OrderItem()
            {
                ID = Config.GetNextOrderItemNumber(),
                OrderId = (i / 4),
                ProductId = product,
                Amount = s_rand.Next(10) + 1,
                Price = price
            };
            OrderItemList.Add(oi);
        }
    }
    /// <summary>
    /// Summons the initialization functions
    /// </summary>
    private static void S_Initialize()
    {
        CreateInitilaizeOrder();
        CreateInitilaizeProduct();
        CreateInitilaizeOrderItem();
    }


}