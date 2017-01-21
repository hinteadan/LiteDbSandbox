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
                .For(new User { Username = "hintee", Email = "h@hintee.ro" })
                .Pin(new Document
                {
                    Name = "test.pdf",
                    NumberOfPages = 13,
                    SizeInBytes = 1000,
                    ProcessResult = new EtiProcessingResult
                    {
                        Duration = TimeSpan.FromSeconds(12),
                        HasSucceeded = true,
                        NumberOfTablesFound = 3
                    }
                })
                .List()
                .ToArray();

            Console.WriteLine($"Done @ {DateTime.Now}");
            Console.ReadLine();
        }
    }
}
