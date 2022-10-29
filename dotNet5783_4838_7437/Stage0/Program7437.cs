// See https://aka.ms/new-console-template for more information
namespace System
{
   partial class Program
    {
       public static void main()
        {
            welcome7437();
            welcome4838();
        }
        static partial void welcome4838();

        private static void welcome7437()
        {
            Console.Write("Enter your name: ");
            string myName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first application ", myName);
            Console.ReadKey();
        }
         
    }
    
}
