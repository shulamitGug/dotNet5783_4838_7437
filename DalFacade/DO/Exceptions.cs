using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// all the do exception
/// </summary>
namespace DO
{
    /// <summary>
    /// Exception to product/order/orderItem does not exist in database
    /// </summary>
    [Serializable]
    public class NotExistException : Exception
    {
        string name;
        int id;
        public NotExistException(int _id, string _name) : base()
        {
            id = _id;
            name = _name;
        }
        public NotExistException(int _id, string _name, string massage) : base(massage)
        {
            id = _id;
            name = _name;
        }
        public override string ToString()
        {
            return $"{name} number {id} does not exist";
        }
    }
    [Serializable]
    /// <summary>
    /// Exception to product/order/orderItem already exist in database
    /// </summary>
    public class AlreadyExistException : Exception
    {
        string name;
        int id;
        public AlreadyExistException(int _id, string _name) : base()
        {
            id = _id;
            name = _name;
        }
        public AlreadyExistException(int _id, string _name, string massage) : base(massage)
        {
            id = _id;
            name = _name;
        }
        public AlreadyExistException(int _id, string _name, string massage,Exception innerExcption) : base(massage, innerExcption)
        {
            id = _id;
            name = _name;
        }
        public override string ToString() =>
            $"{name} number {id} Already exist";
    }
}
