using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteCSharpProgram
{
    class Calculator
    {
        public double ComputeDelta(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }

        public List<double> ComputeZeros(double a, double b, double c)
        {
            double delta = ComputeDelta(a, b, c);
            double x1 = -b - Math.Sqrt(delta) / 2 * a;
            double x2 = -b + Math.Sqrt(delta) / 2 * a;
            List<double> results = new List<double>();

            if (delta < 0)
            {
                Console.WriteLine("NO ZEROS");               
            }
            else if (delta == 0)
            {
                Console.WriteLine("ONE ZERO");

                results.Add(x1);
            }
            else if (delta > 0)
            {
                Console.WriteLine("TWO ZEROS");
                results.Add(x1);
                results.Add(x2);
            }

            return results;
        }
    }
}
