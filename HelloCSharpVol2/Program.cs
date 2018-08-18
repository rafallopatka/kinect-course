using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharpVol2
{
    class Program
    {
        static void Main(string[] args)
        {
            // coments

            int integerNumber = 15;
            float floatNumber = 15.6f;
            double doubleNumber = 30.15;
            string text = "Hello";
            bool booleanValue = true;

            int a = 15 + 3;
            int b = a * 10;
            int c = b - a;

            Console.WriteLine("a = {0}", a);
            Console.WriteLine("b = {0}", b);
            Console.WriteLine("c = {0}", c);

            bool d = a < b;
            bool e = a > b;

            if (d == true)
            {
                Console.WriteLine("d is true");
            }
            else
	        {
                Console.WriteLine("d is false");
	        }

            if (e == true)
            {
                Console.WriteLine("e is true");
            }
            else
            {
                Console.WriteLine("e is false");
            }
        }
    }
}
