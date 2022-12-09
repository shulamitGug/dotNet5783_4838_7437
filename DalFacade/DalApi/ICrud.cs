using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
   public interface ICrud<T>
    {
        public int Add(T addObject);
        public void Update(T updateObject);
        public void Delete(int id);
        public T? Get(int id);
        public IEnumerable<T?> GetAll(Func<T? , bool>? check=null);
        public T? GetByCondition(Func<T?, bool>? check);
    }
}
