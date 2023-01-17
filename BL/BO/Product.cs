using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace BO
{
    public class Product
    {
        /// <summary>
        /// Product specific id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Product Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// product price
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Belonging to a certain category
        /// </summary>
        public Category? CategoryP { get; set;}
        /// <summary>
        /// The amount of products in stock
        /// </summary>
        public int InStock { get; set;}



        public string? Image { get; set; }


        //public string? Image { get; set;}

        public override string ToString()
        {
            return ToolStringClass.ToStringProperty(this);
        }
    }
}

