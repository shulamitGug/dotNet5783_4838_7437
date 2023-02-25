using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class Product : IProduct
{
    const string s_product = @"Product";
    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product()
        {
            ID = Convert.ToInt32(s.Element("ID").Value),
            Name = s.Element("Name").Value,
            Price = Convert.ToInt32(s.Element("Price").Value),
            Image = s.Element("Image").Value,
            InStock = Convert.ToInt32(s.Element("InStock").Value),
            CategoryP=DO.Category.EyeMakeup
            //CategoryP = convertFromStringToCategory(s.Element("CategoryP").Value)
        };
    }
    public int Add(DO.Product product)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        XElement? prod = (from st in productsRootElem.Elements()
                          where st.ToIntNullable("ID") == product.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (prod != null)
            throw new DO.AlreadyExistException(product.ID, "product"); // fix to: throw new DalMissingIdException(id);

        XElement prodElement = new XElement("Product",
                                   new XElement("ID", product.ID),
                                   new XElement("Name", product.Name),
                                   new XElement("Price", product.Price),
                                   new XElement("InStock", product.InStock),
                                   new XElement ("Image", product.Image),
                                   new XElement("CategoryP", product.CategoryP)
                                   );

        productsRootElem.Add(prodElement);

        XMLTools.SaveListToXMLElement(productsRootElem, s_product);

        return product.ID; ;
    }

    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        XElement? prod = (from st in productsRootElem.Elements()
                          where (int?)st.Element("ID") == id
                          select st).FirstOrDefault() ?? throw new DO.NotExistException(id,"product"); // fix to: throw new DalMissingIdException(id);

        prod.Remove(); //<==>   Remove stud from studentsRootElem

        XMLTools.SaveListToXMLElement(productsRootElem, s_product);
    }

    public DO.Product Get(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        return (from s in productsRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Product?)createProductfromXElement(s)).FirstOrDefault()
                ?? throw new DO.NotExistException(id,"product"); 
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? check = null)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        if (check != null)
        {
            return from s in productsRootElem.Elements()
                   let doProd = createProductfromXElement(s)
                   where check(doProd)
                   select (DO.Product?)doProd;
        }
        else
        {
            return from s in productsRootElem.Elements()
                   select createProductfromXElement(s);
        }
    }

    public DO.Product GetByCondition(Func<DO.Product?, bool>? check)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
        return (from s in productsRootElem.Elements()
                let doProd = createProductfromXElement(s)
                where check!(doProd)
                select (DO.Product?)doProd).FirstOrDefault()??throw new DO.NotExistException(0,"product");
    }

    public void Update(DO.Product updateProduct)
    {
        Delete(updateProduct.ID);
        Add(updateProduct);
    }
    private static DO.Category convertFromStringToCategory(string cat)
    {
        switch (cat)
        {
            case "FacialMakeup": return DO.Category.FacialMakeup;
                break;
            case "EyeMakeup": return Category.EyeMakeup;
                break;
            case "LipMakeup": return Category.LipMakeup;
                break;
              case "makeUpBrushes": return Category.makeUpBrushes;
                break;
                    case "cultivation": return Category.cultivation;
                break;
            case "accessories": return Category.accessories;
                break;
            default:return Category.None;
        }
    }
}

