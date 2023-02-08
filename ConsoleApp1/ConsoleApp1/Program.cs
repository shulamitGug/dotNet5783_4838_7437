using System;
class Program
{
    static void Main()
    {
        int[] arr = { 9, 2, 6, 7 }; ;
        for (int i = arr.Length-1; i >=0; i--)
        {
            for (int k = i; k<arr.Length-1; k++)
                Console.Write(" ");
            for (int j = 0; j <= i; j++)
            {
                Console.Write(arr[j]);
            }
            Console.WriteLine();
        }
        int x = 3;
        double y = 3;
        Console.WriteLine(x==y);
        for (int i = 0; i <= arr.Length-1; i++)
        {

            for (int k = i; k < arr.Length-1; k++)
                Console.Write(" ");
            for (int j = 0; j <= i; j++)
            {
                Console.Write(arr[j]);
            }
            Console.WriteLine();
        }
    }
}
