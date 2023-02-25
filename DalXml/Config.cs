using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;
    internal class Config
    {
        static string s_config = "config";
        internal static int GetNextOrderNumber()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderNumber");
        }
        internal static void SaveNextOrderNumber(int number)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderNumber").SetValue(number.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }


        internal static int GetNextOrderItemNumber()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderItemNumber");
        }
        internal static void SaveNextOrderItemNumber(int number)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderItemNumber").SetValue(number.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
    }



    

