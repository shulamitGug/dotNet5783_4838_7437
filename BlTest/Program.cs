class program
{
    static BlApi.IBl? ibl = BlApi.Factory.Get();
    
    ///all the function to order
    public static void OrderFunction()
    {
        int num;
        Console.WriteLine("enter 1 to get all orders, 2 to get order by id,3 to update Sending Date,4 to Update Provide Date,5 to get order status");
        num=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        switch(num)
        {
            case 1:
                {
                    ///get all orders and print
                    foreach (var item in ibl.Order.GetOrders())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 2:
                {
                    //get order by id and print the details
                    int id;
                    Console.WriteLine("enter id");
                    id=int.Parse(Console.ReadLine()?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Order.GetOrderDetails(id));
                    break;
                }
            case 3:
                {
                    //update the sending date and print the order after update
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Order.updateSendingDate(id));
                    break;
                }
            case 4:
                {
                    //update the provide date and print the order after update
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Order.UpdateProvideDate(id));
                    break;
                }
            case 5:
                {
                    //print the stauus of the order with id 
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Order.StatusOrder(id));
                    break;
                }
        }
       
    }
    /// <summary>
    /// all the function to product
    /// </summary>
    public static void ProductFunction()
    {
        int num;
        Console.WriteLine("enter 1 to get all product,2 to get product by id,3 to delete product,4 to update,5 to get catalog,6 to get product item by id,7 to add product");
        num = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        switch (num)
        {
            case 1:
                {
                    //get all products for list
                    foreach (var item in ibl.Product.GetProductForList())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                }
            case 2:
                {
                    //get all the details of product with spesific id
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Product.GetProductById(id));
                    break;
                }
            case 3:
                {
                    //delete product
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    ibl.Product.Delete(id);
                    break;
                }
            case 4:
                {
                    //update details product
                    int _id;
                    int _price;
                    string _name;
                    int _CategoryP;
                    int _InStock;
                    Console.WriteLine("enter id,price,name,and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories,inStock");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _price=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _name=Console.ReadLine() ?? throw new Exception("cannot be empty");
                    _CategoryP = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _InStock =int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    BO.Product boProduct = new BO.Product() { ID = _id, Price = _price, Name = _name, CategoryP = (BO.Category)_CategoryP, InStock = _InStock };
                    ibl.Product.Update(boProduct);
                    break;
                }
            case 5:
                {
                    //get catalog of product
                    foreach (var item in ibl.Product.GetCatalog())
                    {
                        Console.WriteLine(item);
                    }
                    break;    
                }
            case 6:
                {
                    //get product item by id
                    int id;
                    Console.WriteLine("enter id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    Console.WriteLine(ibl.Product.GetProductItemById(id));
                    break ;
                }
            case 7:
                {
                    //add product
                    int _id;
                    int _price;
                    string _name;
                    int _CategoryP;
                    int _InStock;
                    Console.WriteLine("enter id,price,name,and category 1- FacialMakeup, 2-EyeMakeup, 3-LipMakeup,4- makeUpBrushes, 5-cultivation, 6-accessories,inStock");
                    _id = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _price = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _name = Console.ReadLine() ?? throw new Exception("cannot be empty");
                    _CategoryP = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    _InStock = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    BO.Product boProduct = new BO.Product() { ID = _id, Price = _price, Name = _name, CategoryP = (BO.Category)_CategoryP, InStock = _InStock };
                    Console.WriteLine(ibl.Product.Add(boProduct));
                    break;
                }
        }

     }
    /// <summary>
    /// all the function to cart
    /// </summary>
    public static void CartFunction()
    {
        //insert all the details of cart
        int num;
        Console.WriteLine("enter 1 to add product to cart,2 to update amount of product,3 to  confirmation Order");
        num = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        Console.WriteLine("insert customer name,email,adress");
        string? name = Console.ReadLine();
        string? email = Console.ReadLine();
        string? adress = Console.ReadLine();
        BO.Cart boCart = new BO.Cart() { CustomerName = name, CustomerEmail = email, CustomerAdress = adress, Items = new List<BO.OrderItem?>(), TotalPrice = 0 };
        switch (num)
        {
            case 1:
                {
                    //add product to cart
                    Console.WriteLine("insert product id");
                    int id=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                        Console.WriteLine(ibl.Cart.AddProduct(boCart,id));
                    break;
                }
            case 2:
                {
                    //update amount of product in cart
                    
                    int id=int .Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    int amount=int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
                    ibl.Cart.UpdateAmountOfProduct(boCart,id,amount);
                    break;
                }
            case 3:
                {
                    //to confirmation Order
                    Console.WriteLine("insert customer name,email,adress");
                    
                    ibl.Cart.OrderConfirmation(boCart);
                    break;
                }
        }
    }
    public static void Main()
    {   
        int num;
        Console.WriteLine("insert 1 to product,2 to order and 3 to Cart and 0 to stop");
        num = int.Parse(Console.ReadLine() ?? throw new Exception("cannot be empty"));
        try
        {
            while (num != 0)
            {
                if (num == 1)//product
                    ProductFunction();
                else if (num == 2)//order
                    OrderFunction();
                else if (num == 3)//cart
                    CartFunction();
                Console.WriteLine("insert 1 to product,2 to order and 3 to Cart and 0 to stop");
                num = int.Parse(Console.ReadLine()??throw new Exception("cannot be empty"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }



    }
}
