using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteCSharpProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 1;
            double b = 8;
            double c = 6;

            Calculator calc = new Calculator();
            List<double> result = calc.ComputeZeros(a, b, c);

            foreach (var item in result)
            {
                Console.WriteLine("x: {0}", item);
            }
        }
    }
}
