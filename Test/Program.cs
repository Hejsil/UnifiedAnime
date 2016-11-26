using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        class Gen<T>
        {
            public T Value { get; set; }
        }

        interface ITest
        {
            string Value { get; }
        }

        class Test : ITest
        {
            public string Value { get; set; }
        }

        static void Main(string[] args)
        {
            var test = new Gen<Test> { Value = new Test { Value = "Hello World!" } };
            var test2 = test as Gen<ITest>;
        }
    }
}
