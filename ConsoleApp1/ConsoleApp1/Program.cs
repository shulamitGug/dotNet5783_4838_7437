using System;
namespace Ex6
{
   
    class A { public int a;
        public A(int _a)
        {
            a = _a;
        }
}
    public class Program
    {
        static void Main(string[] args)
        {
            List<A> list = new List<A>();
            list.Add(new A(1));
            list.Add(new A(1));
            list.Add(new A(1));
            list.Add(new A(1));
            A m = list.Find(x => x.a == 1);
            int x = 1;
            int y = 2;
            int z = 3;
                x = y = z;
            Console.WriteLine(x+y);
                }
    }
}

//class Program
//{
//    static void Main()
//    {
//        int[] arr = { 9, 2, 6, 7 }; ;
//        for (int i = arr.Length-1; i >=0; i--)
//        {
//            for (int k = i; k<arr.Length-1; k++)
//                Console.Write(" ");
//            for (int j = 0; j <= i; j++)
//            {
//                Console.Write(arr[j]);
//            }
//            Console.WriteLine();
//        }
//        int x = 3;
//        double y = 3;
//        Console.WriteLine(x==y);
//        for (int i = 0; i <= arr.Length-1; i++)
//        {

//            for (int k = i; k < arr.Length-1; k++)
//                Console.Write(" ");
//            for (int j = 0; j <= i; j++)
//            {
//                Console.Write(arr[j]);
//            }
//            Console.WriteLine();
//        }
//    }
//}
