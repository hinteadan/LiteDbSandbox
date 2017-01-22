using LiteDbSandbox.DataProvider;
using LiteDbSandbox.Model;
using System;
using System.Linq;

namespace LiteDbSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            LiteDbMapping.Go();

            var a =
            Analytics
                .For(Fake.User)
                .Pin(Fake.Document)
                .List()
                .ToArray();

            Console.WriteLine($"Done @ {DateTime.Now}");
            Console.ReadLine();
        }
    }
}
