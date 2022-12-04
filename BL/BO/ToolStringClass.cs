using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace BO
{
    public  class ToolStringClass
    {
        /// <summary>
        /// return the details of the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns>string with all the values of this object</returns>
        public static string ToStringProperty<T>(T t)
        {

            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                //if list
                if (!(item.GetValue(t, null) is string) && item.GetValue(t,null) is IEnumerable<object>)
                {
                    foreach (var item2 in (IEnumerable<object>)item.GetValue(t, null))
                        str += item2.ToString();
                }
                else
                {
                    str += "\n" + item.Name

                        + ": " + item.GetValue(t, null);
                }
            }
            return str;
        }
    }
}
