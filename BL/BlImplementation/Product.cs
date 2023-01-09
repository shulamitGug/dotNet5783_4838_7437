using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace BlImplementation
{
    internal class Product:BlApi.IProduct
    {
        DalApi.IDal? idal = DalApi.Factory.Get()!;

        /// <summary>
        /// this function build products of bo and copy the details of do product and return list
        /// </summary>
        /// <returns>list of bo product for list</returns>
        public IEnumerable<BO.ProductForList?> GetProductForList()
        {
            //new list
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            //the loop go at all products
            return from item in idal!.Product.GetAll()
                   select new BO.ProductForList() { Name = item?.Name, Price = item?.Price ?? 0, Category = (BO.Category?)item?.CategoryP, ID = item?.ID ?? 0 };
        }
        /// <summary>
        /// this function return all the detail of this product id
        /// </summary>
        /// <param name="id">id of spetific product</param>
        /// <returns>return bo product</returns>
        public BO.Product GetProductById(int id)
        {
            DO.Product doProduct;
            try
            {
                 doProduct = idal!.Product.Get(id);
            }
            catch(DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist",ex);
            }
            BO.Product boProduct=new BO.Product() { Name = doProduct.Name, Price = doProduct.Price, ID = doProduct.ID, CategoryP = (BO.Category?)doProduct.CategoryP, InStock = doProduct.InStock  };
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
            if (boProduct.Price <= 0)
                throw new BO.NotValidException("the price can not be negative");
            if (boProduct.InStock < 0)
                throw new BO.NotValidException("the amount can not be nagative");
            if (boProduct.Name == "")
                throw new BO.NotEnoughDetailsException("name");
            DO.Product doProduct=new DO.Product() { Name = boProduct.Name, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category?)boProduct.CategoryP };
            int id;
            try
            {
                 id = idal!.Product.Add(doProduct);
            }
            catch (Exception ex)
            {
                throw new BO.AlreadyExistBlException("product is already exist",ex);
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
           //IEnumerable<DO.OrderItem?> orderItemList;
            //IEnumerable<DO.Order?> orderList = idal!.Order.GetAll();
            //check if the product exsist in order
            var items=idal!.OrderItem.GetAll(x=>x?.ProductId==id);
            if (items != null)
                throw new BO.AlreadyExistBlException("the product is exist in orders so it cannot delete");

                //foreach (DO.Order? order in orderList)
                //{
                //    orderItemList = idal.OrderItem.GetAll(x => x?.OrderId == order?.ID);
                //    foreach (DO.OrderItem? item in orderItemList)
                //        if (item?.ProductId == id)
                //            throw new BO.AlreadyExistBlException("the product is exist in orders so it cannot delete");
                //}
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
            DO.Product doProduct = new DO.Product() { Name = boProduct.Name, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category?)boProduct.CategoryP };
            idal?.Product.Update(doProduct);
        }
        /// <summary>
        /// get calaog to patient
        /// </summary>
        /// <returns>list of product to customer</returns>
        public IEnumerable<BO.ProductItem?> GetCatalog(Func<DO.Product?, bool>? check = null)
        {
            IEnumerable<DO.Product?> doProduct;
            if (check == null)
                 doProduct = idal!.Product.GetAll();
            else
                doProduct = idal!.Product.GetAll(check);
            List<BO.ProductItem> list = new List<BO.ProductItem>();
            //create product items
                return from prod in doProduct
                       select new BO.ProductItem { Id = prod?.ID ?? 0, Name = prod?.Name, Price = prod?.Price ?? 0, Amount = prod?.InStock ?? 0, Category = (BO.Category?)prod?.CategoryP ,InStock= prod?.InStock > 0 ?true:false};
            ////foreach (DO.Product? item in doProduct)
            ////{
            ////    if (check == null || check(item))
            ////    {
            ////        BO.ProductItem boProd = new BO.ProductItem() { Id = item?.ID ?? 0, Name = item?.Name, Price = item?.Price ?? 0, Amount = item?.InStock ?? 0, Category = (BO.Category?)item?.CategoryP };
            ////        if (item?.InStock > 0)
            ////        {
            ////            boProd.InStock = true;
            ////        }
            ////        else
            ////            boProd.InStock = false;
            ////        list.Add(boProd);
            ////    }
            ////}
            ////return list;
        }
        /// <summary>
        /// get product item by id
        /// </summary>
        /// <param name="id">id of product that want to get the product item</param>
        /// <returns>all the details of this product item</returns>
        public BO.ProductItem GetProductItemById(int id)
        {
            DO.Product doProduct;
            try
            {
                doProduct = idal!.Product.Get(id) ;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            BO.ProductItem boProduct = new BO.ProductItem() {Id = id, Name = doProduct.Name, Price = doProduct.Price, Amount = doProduct.InStock,Category=(BO.Category?)doProduct.CategoryP };
            if (doProduct.InStock > 0)
            {
                boProduct.InStock = true;
            }
            else
                boProduct.InStock = false;
            return boProduct;
        }
        public IEnumerable<BO.ProductForList?> GetProductForListByCategory(BO.Category? category)
        {
           IEnumerable<DO.Product?> product= idal!.Product.GetAll(x => x?.CategoryP == (DO.Category?)category);
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            //the loop go at all products
            return from prod in product
                   select new BO.ProductForList() { Name = prod?.Name, Price = prod?.Price ?? 0, Category = (BO.Category?)prod?.CategoryP, ID = prod?.ID ?? 0 };
            //foreach (DO.Product? item in prod)
            //{
            //    BO.ProductForList pr = new BO.ProductForList() { Name = item?.Name, Price = item?.Price ?? 0, Category = (BO.Category?)item?.CategoryP, ID = item?.ID ?? 0 };
            //    products.Add(pr);
            //}
            //return products;
        }

    }
}

