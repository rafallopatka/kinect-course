using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            bool iAmTrue = true;
            bool iAmFalse = false;

            Console.WriteLine("I am true: {0}", iAmTrue);
            Console.WriteLine("I am false: {0}", iAmFalse);


            bool b1 = 2 == 2;            
            Console.WriteLine("2 equals to 2: {0}", b1);

            bool b2 = 2 != 2;
            Console.WriteLine("2 not equals to 2: {0}", b2);

            bool b3 = 2 > 1;
            Console.WriteLine("2 is greater than 1: {0}", b3);

            bool b4 = 2 < 1;
            Console.WriteLine("2 is smaller than 1: {0}", b4);

            bool b5 = 1 >= 2;
            Console.WriteLine("1 is greater or equal to 2: {0}", b5);

            bool b6 = 1 <= 2;
            Console.WriteLine("1 is smaller or equal to 2: {0}", b6);

            bool b7 = 1 >= 1;
            Console.WriteLine("1 is greater or equal to 1: {0}", b7);

            bool b8 = 2 + 2 == 5;
            Console.WriteLine("2 + 2 is equal to 5: {0}", b8);

            bool b9 = 2 + 2 != 5;
            Console.WriteLine("2 + 2 is not equal to 5: {0}", b9);

            if (b8 == true)
            {
                Console.WriteLine("Math Error");
            }

            if (b8 == false)
            {
                Console.WriteLine("Success!!!!");
            }

            if (b8 == true)
            {
                Console.WriteLine("Math Error");
            }
            else
            {
                Console.WriteLine("Success!!!!");
            }

            if (b1 == true || b2 == true)
            {
                Console.WriteLine("b1 == true || b2 == true are TRUE");
            }
            else
            {
                Console.WriteLine("b1 == true || b2 == true are FALSE");
            }

            if (b1 == true && b2 == true)
            {
                Console.WriteLine("b1 == true && b2 == true are TRUE");
            }
            else
            {
                Console.WriteLine("b1 == true && b2 == true are FALSE");
            }
        }
    }
}
