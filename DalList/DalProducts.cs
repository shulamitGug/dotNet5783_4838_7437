
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
    public int Add(Product product)
    {
        if (CheckProduct(product.ID) )
            throw new AlreadyExistException(product.ID, "product");
        DataSource.ProductsList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// return all the details of product by product id
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>product object</returns>
    /// <exception cref="Exception">if the product is not exist </exception>
    public Product Get(int id)
    {
        //look for the product with the same id
      return DataSource.ProductsList.FirstOrDefault(x => x?.ID ==id)??throw new NotExistException(id, "product", "alreadyExsist");
    }
    /// <summary>
    /// return all the product in the array
    /// </summary>
    /// <returns>array of product</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? check = null)
    {
        //copy to new arr all the products that exist the arr
        if (check != null)
            //    return from prod in DataSource.ProductsList
            //           where check(prod) select prod;
            return DataSource.ProductsList.FindAll(x => check(x));
        else
            return from prod in DataSource.ProductsList
                   select prod; 
    }
    /// <summary>
    /// the function delete product from arr
    /// </summary>
    /// <param name="id">id of product</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void Delete(int id)
    {
        int count = DataSource.ProductsList.RemoveAll(prod => prod?.ID == id);
        if (count == 0)
            throw new NotExistException(id, "product");
    }
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="product">product object</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void Update(Product product)
    {
        int count = DataSource.ProductsList.RemoveAll(prod => product.ID == prod?.ID);
        if (count == 0)
            throw new NotExistException(product.ID, "product");

        DataSource.ProductsList.Add(product);
        //Go through the database until the requested order
    }
    public Product GetByCondition(Func<Product?, bool>? check)
    {
        return DataSource.ProductsList.Find(x=>check(x))??  
        throw new DO.NotExistException(1,"product does not exist");
    }
    private bool CheckProduct(int id)
    {
        return DataSource.ProductsList.Any(prod => prod?.ID == id);
    }
}
