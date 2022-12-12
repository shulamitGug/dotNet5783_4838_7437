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
        public IEnumerable<BO.ProductItem?> GetCatalog();
        public BO.ProductItem GetProductItemById(int id);
        public IEnumerable<BO.ProductForList?> GetProductForListByCondition(Func<DO.Product?, bool>? check);
    }
}
