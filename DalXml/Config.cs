using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
    /// <summary>
    /// read the values from config.xml
    /// </summary>
   internal class Config
    {
        //path
        static string s_config = "config";
        /// <summary>
        /// find teh next key
        /// </summary>
        /// <returns>next key</returns>
        internal static int GetNextOrderNumber()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderNumber")!;
        }
    /// <summary>
    /// save the next- key+1
    /// </summary>
    /// <param name="number">next key</param>
        internal static void SaveNextOrderNumber(int number)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderNumber")!.SetValue(number.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }

    /// <summary>
    /// find teh next key
    /// </summary>
    /// <returns>next key</returns>
    internal static int GetNextOrderItemNumber()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderItemNumber")!;
        }
    /// <summary>
    /// save the next- key+1
    /// </summary>
    /// <param name="number">next key</param>
    internal static void SaveNextOrderItemNumber(int number)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderItemNumber")!.SetValue(number.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
    }



    

