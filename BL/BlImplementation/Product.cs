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
                   select new BO.ProductForList() { Name = item?.Name, Image = item?.Image, Price = item?.Price ?? 0, Category = (BO.Category?)item?.CategoryP, ID = item?.ID ?? 0 };
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
            BO.Product boProduct=new BO.Product() { Name = doProduct.Name, Image = doProduct.Image, Price = doProduct.Price, ID = doProduct.ID, CategoryP = (BO.Category?)doProduct.CategoryP, InStock = doProduct.InStock  };
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
            DO.Product doProduct=new DO.Product() { Name = boProduct.Name,Image=boProduct.Image, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category?)boProduct.CategoryP };
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
            //check if the product exsist in order
            IEnumerable<DO.OrderItem?>? items=idal!.OrderItem.GetAll(x=>x?.ProductId==id);
            if (items != null&&items.Count()!=0)
                throw new BO.AlreadyExistBlException("the product is exist in orders so it cannot delete");
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
            DO.Product doProduct = new DO.Product() { Name = boProduct.Name, Image = boProduct?.Image, Price = boProduct.Price, ID = boProduct.ID, InStock = boProduct.InStock, CategoryP = (DO.Category?)boProduct.CategoryP };
            idal?.Product.Update(doProduct);
        }
        /// <summary>
        /// get calaog to patient
        /// </summary>
        /// <returns>list of product to customer</returns>
        public IEnumerable<BO.ProductItem?> GetCatalog(Func<DO.Product?, bool>? check = null, BO.Cart? cart = null)
        {
            IEnumerable<DO.Product?> doProduct;
            if (check == null)
                 doProduct = idal!.Product.GetAll();
            else
                doProduct = idal!.Product.GetAll(check);
            List<BO.ProductItem> list = new List<BO.ProductItem>();
            //create product items
            return from prod in doProduct
                   let amount = cart == null ? 0 : cart.Items?.FirstOrDefault(x => x?.ProductId == prod?.ID)?.Amount??0
                       select new BO.ProductItem { Id = prod?.ID ?? 0,Image=prod?.Image, Name = prod?.Name, Price = prod?.Price ?? 0, Amount = amount, Category = (BO.Category?)prod?.CategoryP ,InStock= prod?.InStock > 0 ?true:false};
        }
        /// <summary>
        /// get product item by id
        /// </summary>
        /// <param name="id">id of product that want to get the product item</param>
        /// <returns>all the details of this product item</returns>
        public BO.ProductItem GetProductItemById(int id, BO.Cart cart)
        {
            DO.Product doProduct;
            int amount;
            try
            {
                if (id > 0)
                    doProduct = idal!.Product.Get(id);
                else
                    throw new BO.NotValidException("id");
                amount= cart.Items?.FirstOrDefault(x => x?.ProductId == id)?.Amount??0;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("product does not exist", ex);
            }
            BO.ProductItem boProduct = new BO.ProductItem() {Id = id, Image = doProduct.Image, Name = doProduct.Name, Price = doProduct.Price, Amount = amount,Category=(BO.Category?)doProduct.CategoryP };
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
                   select new BO.ProductForList() { Name = prod?.Name, Image = prod?.Image ,Price = prod?.Price ?? 0, Category = (BO.Category?)prod?.CategoryP, ID = prod?.ID ?? 0 };
        }
        //public IEnumerable<BO.ProductForList?> GetPoupolarProduct()
        //{
        //   var x= from prod in GetCatalog()
        //    orderby prod.Name
        //    group prod by prod.Category into g
        //    select new { Key = g.Key, prod = g };
        //}
        public IEnumerable<BO.ProductForList?> GetPoupolarProduct()
        {
            //create a list of groups of items that appear in order, by ID
            var grouping = from item in idal!.OrderItem.GetAll()
                           group item by ((DO.OrderItem?)(item))?.ProductId into g
                           select new { id = g.Key, Items = g };
            //take the 10 that appear in the biggest amount of orders 
            grouping = grouping.OrderByDescending(x => x.Items.Count()).Take(5);
            //return the 5 popular items:
            try
            {
                return from item in grouping
                       let prod = idal.Product.Get(item?.id ?? throw new BO.NotValidException("Product ID is incorrect"))
                       select new BO.ProductForList
                       {
                           ID = prod.ID,
                           Name = prod.Name,
                           Price = prod.Price,
                           Category= BO.Category.None,
                           //Category = (BO.Category)prod.CategoryP!,
                           Image= prod.Image
                       };
            }
            catch
            {
                throw new BO.NotExistBlException("Product is not exist");
            }
        }



    }
}

