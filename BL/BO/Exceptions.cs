using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// all the Exception
/// </summary>
namespace BO
{
    [Serializable]
    //Exception to order/product/orderItem not exist in data base
    public class NotExistBlException : Exception
    {
        public NotExistBlException() : base() { }
        public NotExistBlException(string massege) : base(massege) { }
        public NotExistBlException(string massage, Exception innerExeption)
            : base(massage, innerExeption) { }
        public override string ToString() =>
             base.ToString() + $" -not exsist";
    }
    [Serializable]
    //Exception to order/product/orderItem already exist in data base
    public class AlreadyExistBlException : Exception
    {
        public AlreadyExistBlException() : base() { }
        public AlreadyExistBlException(string massege) : base(massege) { }
        public AlreadyExistBlException(string massage, Exception innerExeption)
            : base(massage, innerExeption) { }
        public override string ToString() =>
             base.ToString() + $" -alredy exist";
    }
    [Serializable]
    //Exception because user didnot insert enough details
    public class NotEnoughDetailsException : Exception
    {
        string? fieldName;
        public NotEnoughDetailsException() : base() { }

        public NotEnoughDetailsException(string _fieldName) : base()
        {
            fieldName = _fieldName;
        }
        public NotEnoughDetailsException(string _fieldName,string massage):base(massage) 
        {
            fieldName = _fieldName;
        }
        public NotEnoughDetailsException(string _fieldName, string massage,Exception innerEx) : base(massage, innerEx)
        {
            fieldName = _fieldName;
        }
        public override string ToString() =>
            $"the {fieldName} can not be null";
    }
    [Serializable]
    //Exception that user insert invalid field
    public class NotValidException : Exception
    {
        string? fieldName;
        public NotValidException() : base() { }

        public NotValidException(string _fieldName) : base()
        {
            fieldName = _fieldName;
        }
        public NotValidException(string _fieldName, string massage) : base(massage)
        {
            fieldName = _fieldName;
        }
        public NotValidException(string _fieldName, string massage, Exception innerEx) : base(massage, innerEx)
        {
            fieldName = _fieldName;
        }
        public override string ToString() =>
            $"the {fieldName} is not valid";
    }
    //Exception to product not have enough items in stock
    [Serializable]
    public class NotInStockException : Exception
    {
        int id;
        string name;
        public NotInStockException(int _id, string _name) : base()
        {
            id = _id;
            name = _name;
        }
        public NotInStockException(int _id, string _name, string massage) : base(massage)
        {
            id = _id;
            name = _name;
        }
        public NotInStockException(int _id, string _name, string massage, Exception innerException) : base(massage, innerException)
        {
            id = _id;
            name = _name;
        }
        public override string ToString() =>
            $"product number {id} name: {name} does not have enough in stock";
    }
}


