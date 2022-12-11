
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
        foreach (var item in DataSource.ProductsList)
        {
            if (item.ID == product.ID)
                throw new AlreadyExistException(product.ID, "product");
        }
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
        foreach (var item in DataSource.ProductsList)
            {
                if (item.ID == id)
                    return item;
            }
        throw new NotExistException(id, "product", "alreadyExsist");
    }
    /// <summary>
    /// return all the product in the array
    /// </summary>
    /// <returns>array of product</returns>
    public IEnumerable<Product> GetAll()
    {
            List<Product> newProduct = new List<Product>();
            //copy to new arr all the products that exist the arr
            foreach (DO.Product item in DataSource.ProductsList)
            {
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
        foreach (Product product in DataSource.ProductsList)
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
    public void Update(Product product)
    {
        bool isExist = false;
        //look for the product
        foreach (Product prod in DataSource.ProductsList)
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
    public Product GetByCondition(Func<Product, bool>? check)
    {
        foreach (Product prod in DataSource.ProductsList)
        {
            if (check(prod))
                return prod;
        }
        throw new DO.NotExistException(1, "order");
    }
}
