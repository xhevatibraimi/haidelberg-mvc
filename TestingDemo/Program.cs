using System;

namespace TestingDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var a = 0.1;
            var b = 0.2;
            var result = Add(a, b);
            Console.WriteLine(result == 0.3);

            a = 1.0;
            b = 2.0;
            result = Add(a, b);
            Console.WriteLine(result == 3.0);
        }

        public static double Add(double a, double b)
        {
            var sum = a + b;
            var result = Math.Round(sum, 4);
            return result;
        }
    }
}
