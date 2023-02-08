using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi
{
    public interface IProduct 
    {
        public IEnumerable<BO.ProductForList?> GetProductForList();
        public BO.Product GetProductById(int id);
        public int Add(BO.Product product);
        public void Delete(int id);
        public void Update(BO.Product product);
        public IEnumerable<BO.ProductItem?> GetCatalog(Func<DO.Product?, bool>? check = null,BO.Cart? cart=null);
        public BO.ProductItem GetProductItemById(int id,BO.Cart cart);
        public IEnumerable<BO.ProductForList?> GetProductForListByCategory(BO.Category? category);
    }
}
