using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBuildingBlocks
{
    class Program
    {
        static void Main(string[] args)
        {
            string helloWorldVariable = "Hello World";
            Console.WriteLine(helloWorldVariable);

            int myAge = 24;
            string myAgeSentence = "I am "+ myAge + " years old.";

            string myAgeSentence2 = string.Format("I am {0} years old.", myAge);

            Console.WriteLine(myAgeSentence);

            double myWeight = 75.3;
            double myHeight = 175.2;

            Console.WriteLine("My weight: {0}, My height: {1}", myWeight, myHeight);

            double bmi = myWeight / (myHeight * myHeight);
            double bmi2 = myWeight / Math.Pow(myHeight, 2);

            Console.WriteLine("my bmi {0}", bmi);
            Console.WriteLine("my bmi {0}", bmi2);
        }
    }
}
