using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 100; i < 1000; i++)
            {
                Console.WriteLine("Number: {0}", i);
            }

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum = sum + i;
            }

            Console.WriteLine("Sum: {0}", sum);

            int sum2 = 0;
            for (int i = 0; i < 10; i++)
            {
                sum2 += i;
            }
            Console.WriteLine("Sum2: {0}", sum2);

            for (int i = 30; i >= 0; i--)
            {
                Console.WriteLine("Left {0}", i);
            }

            List<int> numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(15);
            numbers.Add(36);
            numbers.Add(45);

            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine("Element {0}", numbers[i]);
            }

            foreach (var number in numbers)
            {
                Console.WriteLine("Element {0}", number);
            }


            while (numbers.Count > 0)
            {
                var first = numbers.First();
                Console.WriteLine("Remove {0}", first);
                numbers.Remove(first);
            }

            numbers.Add(1);
            numbers.Add(15);
            numbers.Add(36);
            numbers.Add(45);

            numbers.Clear();


        }
    }
}
