
using DO;
namespace Dal;
/// <summary>
/// product function
/// </summary>
public class DalProducts
{
    /// <summary>
    /// add product to the array
    /// </summary>
    /// <param name="product">get product object</param>
    /// <returns>if add return the id of this product</returns>
    /// <exception cref="Exception">if there is no place</exception>
    public int AddProduct(Products product)
    {
        product.ID = DataSource.Config.GetNextProductNumber();
        if (DataSource.Config.nextProductIndex >= DataSource.arrProducts.Length)
            throw new Exception("there is no place");
        Console.WriteLine(DataSource.Config.nextProductIndex);
        DataSource.arrProducts[DataSource.Config.nextProductIndex++] = product;
        return product.ID;
    }
    /// <summary>
    /// return all the details of product by product id
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>product object</returns>
    /// <exception cref="Exception">if the product is not exist </exception>
    public Products GetProduct(int id)
    {
        //look for the product with the same id
        for (int i = 0; i < DataSource.arrProducts.Length; i++)
        {
            if (DataSource.arrProducts[i].ID == id)
                return DataSource.arrProducts[i];
        }
        throw new Exception("the product is not exist");
    }
    /// <summary>
    /// return all the product in the array
    /// </summary>
    /// <returns>array of product</returns>
    public Products[] GetAllProduct()
    {
        Products[] prod = DataSource.arrProducts;
        Products[] newProduct = new Products[DataSource.Config.nextProductIndex];
        //copy to new arr all the products that exist the arr
        for (int i = 0; i < DataSource.Config.nextProductIndex; i++)
        {
            newProduct[i] = prod[i];
        }
        return newProduct;
    }
    /// <summary>
    /// the function delete product from arr
    /// </summary>
    /// <param name="id">id of product</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void DeleteProduct(int id)

    {
        int i;
        //The loop goes through the elements of the array and stops when an product with the same id as the received id is found or until the end of the buffer
        for (i = 0; i < DataSource.arrProducts.Length && DataSource.arrProducts[i].ID != id; i++) ;
        if (i >= DataSource.arrProducts.Length)
            throw new Exception("the product is not exist");
        //The loop starts with the element immediately after the product to be deleted and moves each product to the previous position in the array
        for (int j = i + 1; j < DataSource.arrProducts.Length; j++)
        {
            DataSource.arrProducts[j - 1] = DataSource.arrProducts[j];
        }
        DataSource.Config.nextProductIndex--;
    }
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="product">product object</param>
    /// <exception cref="Exception">the product is not exist</exception>
    public void UpdateProduct(Products product)
    {
        int i;
        for (i = 0; i < DataSource.arrProducts.Length && DataSource.arrProducts[i].ID != product.ID; i++) ;
        if (i >= DataSource.arrProducts.Length)
            throw new Exception("the product is not exist");
        DataSource.arrProducts[i] = product;

    }
}
