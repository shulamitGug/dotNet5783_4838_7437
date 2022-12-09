
using DO;
namespace Dal;
using DalApi;
/// <summary>
/// product function
/// </summary>
internal class DalProducts:IProduct
{
    /// <summary>
    /// add product to the array
    /// </summary>
    /// <param name="product">get product object</param>
    /// <returns>if add return the id of this product</returns>
    public int Add(Products product)
    {
        Products? p = GetByCondition(x => x.ID == product.ID);
            if (p!=null)
                throw new AlreadyExistException(product.ID,"product");
        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// return all the details of product by product id
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>product object</returns>
    /// <exception cref="Exception">if the product is not exist </exception>
    public Products? Get(int id)
    {
        //look for the product with the same id
        //foreach (var item in DataSource.ProductsList)
        //{
        //    if (item.ID == id)
        //        return item;
        //}
        //throw new NotExistException(id,"product","fff");

        Products? p= GetByCondition(x=>x.ID == id);
        if(p==null)
            throw new NotExistException(id, "product", "notEx");
        return p;
    }
    /// <summary>
    /// return all the product in the array
    /// </summary>
    /// <returns>array of product</returns>
    public IEnumerable<Products> GetAll(Func<Products, bool>? check = null)
    {
            List<Products> newProduct = new List<Products>();
            //copy to new arr all the products that exist the arr
            foreach (DO.Products item in DataSource.ProductsList)
            {
            if((check!=null&& check(item))||check==null)
                newProduct.Add(item);
            }
            return newProduct;
    }
    /// <summary>
    /// the function delete product from arr
    /// </summary>
    /// <param name="id">id of product</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void Delete(int id)

    {
        foreach (Products product in DataSource.ProductsList)
        {
            if (product.ID == id)
            {
                DataSource.ProductsList.Remove(product);
                return;
            }
        }
        throw new NotExistException(id,"product");
    }
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="product">product object</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void Update(Products product)
    {

        bool isExist = false;
        foreach (Products prod in DataSource.ProductsList)
        {
            if (prod.ID == product.ID)
            {
                isExist = true;
                DataSource.ProductsList.Remove(prod);
                break;
            }
        }

        //Go through the database until the requested order
        if (!isExist)
            throw new NotExistException(product.ID,"product");
        DataSource.ProductsList.Add(product);

    }
    public Products? GetByCondition(Func<Products?, bool>? check)
    {
        foreach (Products? prod in DataSource.ProductsList)
        {
            if (check(prod))
                return prod;
        }
        //throw new DO.NotExistException(1, "order");
        return null;
    }
}
