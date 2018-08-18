using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Person john = new Person();
            Person bob = new Person();

            john.FirstName = "Johny";
            john.LastName = "Malik";
            john.Mail = "j.malik@mail.com";
 
            bob.FirstName = "Bobie";
            bob.LastName = "Walker";
            bob.Mail = "b.walker@mail.com";

            //Console.WriteLine(bob.LastName);
            //Console.WriteLine(john.LastName);

            john.SayHello();
            bob.SayHello();
            john.Introduce();
            bob.Introduce();
            bob.SayGoodBye();
            john.SayGoodBye();

            bob.SendEmail(john, "Hello john this email is for you");
        }
    }
}
