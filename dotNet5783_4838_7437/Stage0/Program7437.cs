// See https://aka.ms/new-console-template for more information
using System;
namespace stage0
{
   partial class Program
    {
        static void Main()
        {
            Welcome7437();
            Welcome4838();
            Console.ReadKey();

        }
        static partial void Welcome4838();
        private static void Welcome7437()
        {
            Console.Write("Enter your name: ");
            string myName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first application ", myName);
        }
      
    }
    
}
