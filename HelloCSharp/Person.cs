using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCSharp
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }

        public event EventHandler<string> EmailReceived;

        public Person()
        {
            EmailReceived += OnEmailReceived;
        }

        void OnEmailReceived(object sender, string e)
        {
            Console.WriteLine("Message reveived: {0}", e);
        }

        public void SayHello()
        {
            Console.WriteLine(FirstName + ": Hello");
        }

        public void SayGoodBye()
        {
            Console.WriteLine(FirstName + ": Good bye");
        }

        public void Introduce()
        {
            Console.WriteLine("My name is {0} {1}, and that's my mail {2}", FirstName, LastName, Mail);
        }

        public void SendEmail(Person receiver, string message)
        {
            receiver.EmailReceived.Invoke(this, message);
        }
    }
}
