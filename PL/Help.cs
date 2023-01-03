using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    static public class Help
    {
       static bool x=false;
        public static void setX(string state)
        {
            if (state == "update")
                x= true;
            else
            x= false;
        }
        public static bool getX()
        {
            return x;
        }

    }
}
