using DO;
namespace Dal;
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
        private static int nextProductNumber = 1000000;
        private static int nextOrderItemNumber = 0;
        /// <summary>
        /// Saving the position of the last element in the array
        /// </summary>
        internal static int nextOrderIndex = 0;
        internal static int nextProductIndex = 0;
        internal static int nextOrderItemIndex = 0;
        /// <summary>
        /// return the next id
        /// </summary>
        /// <returns>Next Order id</returns>
        public static int GetNextOrderNumber()
        {
            nextOrderNumber++;
            return nextOrderNumber;
        }
        public static int GetNextProductNumber() {
            nextProductNumber++;
            return nextProductNumber;
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
    internal static Order[] arrOrders=new Order[100];
    internal static Products[] arrProducts= new Products[50];
    internal static OrderItem[] arrOrderItem = new OrderItem[200];
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
                TimeSpan ts1 = new TimeSpan(s_rand.Next(20));
            DateTime d;
            if (i % 3 == 0)
            {
                 d = new DateTime();
            }
            else
            {
                 d = DateTime.MinValue + ts;
            }

            Order o = new Order()
            {
                ID = Config.GetNextOrderNumber(),
                CustomerName = arrCustomerName[i],
                CustomerAdress = arrCustomerAdress[i],
                CustomerEmail = arrCustomerEmail[s_rand.Next(arrCustomerName.Length)],
                OrderDate = DateTime.MinValue,
                ShipDate = d,
                DeliveryDate = DateTime.MinValue + ts+ts1,
            };
            arrOrders[Config.nextOrderIndex++] = o;

        }
    }
    /// <summary>
    /// Initializing an product pool with little data
    /// </summary>
    private static void CreateInitilaizeProduct()
    {
        string[] arrProductName = { "Makup", "blush", "Primer", "silhouettes", "Rimmel", "Shimmer", "eye brush", "Brush blush", "Eyeliner", "Concealer", "lipstick", "powder", "Bronzer", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush", "Blur brush" };
        for (int i = 0; i < 20; i++)
        {
            int instock;
            if (i % 4 == 0)
                instock = 0;
            else
                instock = s_rand.Next(10) + 200;
            Products p = new Products()
            {
                ID = Config.GetNextProductNumber(),
                Name = arrProductName[i],
                CategoryP = (Category)(i%2),
                Price = s_rand.Next(10) + 450,
                InStock =instock,
            };
            arrProducts[Config.nextProductIndex++] = p;
        }
    }
    /// <summary>
    /// Initializing an order item pool with little data
    /// </summary>
    private static void CreateInitilaizeOrderItem()
    {
        double price;
         int j;
        for (int i = 0; i < 40; i++)
        {
            int product = s_rand.Next(20)+1000000;
            for ( j = 0; j < 20 && arrProducts[j].ID != product; j++) ;
            price = arrProducts[j].Price;
            OrderItem oi = new OrderItem()
            {
                ID = Config.GetNextOrderItemNumber(),
                OrderId = s_rand.Next(10),
                ProductId =product,
                Amount = s_rand.Next(10) + 1,
                Price = price
            };
            arrOrderItem[Config.nextOrderItemIndex++] =oi;
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
   