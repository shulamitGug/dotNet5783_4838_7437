using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class Product:BlApi.IProduct
    {
        IDal idal = new Dallist();
        /// <summary>
        /// this function build products of bo and copy the details of do product and return list
        /// </summary>
        /// <returns>list of bo product for list</returns>
        public IEnumerable<BO.ProductForList> GetProductForList()
        {
            //new list
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            //the loop go at all products
                foreach (var item in idal.Product.GetAll())
                {
                    BO.ProductForList pr = new BO.ProductForList() { Name = item.Name, price = item.Price, category = (BO.Category)item.CategoryP, ID = item.ID };
                    products.Add(pr);
                }
            return products;

        }
        /// <summary>
        /// this function return all the detail of this product id
        /// </summary>
        /// <param name="id">id of spetific product</param>
        /// <returns>return bo product</returns>
        public BO.Product GetProductById(int id)
        {
            DO.Products doProduct;
            try
            {
                 doProduct = idal.Product.Get(id);
            }
            catch(DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
            BO.Product boProduct=new BO.Product() { Name = doProduct.Name, Price = doProduct.Price, ID = doProduct.ID, CategoryP = (BO.Category)doProduct.CategoryP, InStock = doProduct.InStock  };
            return boProduct;
        }
        /// <summary>
        /// the fucntion add product to data base
        /// </summary>
        /// <param name="boProduct"></param>
        /// <returns>the new id</returns>
        /// <exception cref="Exception">check the details</exception>
        public int Add(BO.Product boProduct)
        {
            //check the details
            if (boProduct.ID < 0)
                throw new BO.NotValidException("the id can not be negative");
            if (boProduct.ID < 100000)
                throw new BO.NotValidException("the id must be bigger");
            if (boProduct.Price < 0)
                throw new BO.NotValidException("the price can not be negative");
            if (boProduct.InStock < 0)
                throw new BO.NotValidException("the amount can not be nagative");
            if (boProduct.Name == "")
                throw new BO.NotEnoughDetailsException("name");
            DO.Products doProduct=new DO.Products() { Name = boProduct.Name, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category)boProduct.CategoryP };
            int id;
            try
            {
                 id = idal.Product.Add(doProduct);
            }
            catch (Exception ex)
            {
                throw new BO.AlreadyExistBlException("product is already exist");
            }
            return id;
        }
        /// <summary>
        /// delete product from data base
        /// </summary>
        /// <param name="id">the product id that want to delete</param>
        /// <exception cref="Exception"></exception>
        public void Delete(int id)
        {
           IEnumerable<DO.OrderItem> orderItemList;
            IEnumerable<DO.Order> orderList = idal.Order.GetAll();
            //check if the product exsist in order
            foreach (DO.Order order in orderList)
            {
                orderItemList = idal.OrderItem.GetOrderItemByOrder(order.ID);
                foreach (DO.OrderItem item in orderItemList)
                    if (item.ID == id)
                        throw new BO.AlreadyExistBlException("the product is exist in order cannot delete");
            }
            try
            {
                idal.Product.Delete(id);
            }
            catch(DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
        }
        /// <summary>
        /// update details of product
        /// </summary>
        /// <param name="boProduct">get new bo product</param>
        /// <exception cref="Exception"></exception>
        public void Update(BO.Product boProduct)
        {
            if (boProduct.ID < 0)
                throw new BO.NotValidException("the id can not be negative");
            if (boProduct.Price < 0)
                throw new BO.NotValidException("the price can not be negative");
            if (boProduct.InStock < 0)
                throw new BO.NotValidException("the amount can not be nagative");
            if (boProduct.Name == "")
                throw new BO.NotEnoughDetailsException("the name can not be empty");
            DO.Products doProduct = new DO.Products() { Name = boProduct.Name, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category)boProduct.CategoryP };
            idal.Product.Update(doProduct);
        }
        /// <summary>
        /// get calaog to patient
        /// </summary>
        /// <returns>list of product to customer</returns>
        public IEnumerable<BO.ProductItem> GetCatalog()
        {
           IEnumerable<DO.Products> doProduct = idal.Product.GetAll();
            List<BO.ProductItem> list = new List<BO.ProductItem>();
            //create product items
            foreach (DO.Products item in doProduct)
            {
                BO.ProductItem boProd=new BO.ProductItem() { Id = item.ID , Name = item.Name, Price = item.Price, amount = item.InStock,Category=(BO.Category)item.CategoryP};
                if(item.InStock > 0)
                {
                    boProd.inStock = true;
                }
                else
                    boProd.inStock = false;
                list.Add(boProd);
            }
            return list;
        }
        /// <summary>
        /// get product item by id
        /// </summary>
        /// <param name="id">id of product that want to get the product item</param>
        /// <returns>all the details of this product item</returns>
        public BO.ProductItem GetProductItemById(int id)
        {
            DO.Products doProduct;
            try
            {
                doProduct = idal.Product.Get(id);
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            BO.ProductItem boProduct = new BO.ProductItem() {Id = id, Name = doProduct.Name, Price = doProduct.Price, amount = doProduct.InStock,Category=(BO.Category)doProduct.CategoryP };
            if (doProduct.InStock > 0)
            {
                boProduct.inStock = true;
            }
            else
                boProduct.inStock = false;
            return boProduct;
        }

    }
}

