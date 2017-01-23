using LiteDbSandbox.DataProvider;
using LiteDbSandbox.Model;
using System;
using System.Timers;

namespace LiteDbSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            LiteDbMapping.Go();

            StartGeneratingVisits();

            StartGeneratingReports();

            Console.WriteLine($"Done @ {DateTime.Now}");
            Console.ReadLine();
        }

        private static void StartGeneratingReports()
        {
            var random = new Random();

            var timer = new Timer { AutoReset = false, Interval = 500 };
            timer.Elapsed += (s, e) =>
            {
                Reports.Generate();

                Console.WriteLine($"New Reports Generated @ {DateTime.Now}");

                timer.Interval = random.Next(5000, 30000);
                timer.Start();
            };
            timer.Start();
        }

        private static void StartGeneratingVisits()
        {
            var random = new Random();

            var timer = new Timer { AutoReset = false, Interval = random.Next(1000, 10000) };
            timer.Elapsed += (s, e) =>
            {
                Analytics
                    .For(Fake.User)
                    .Pin(Fake.Document);

                SQLiteStore.SQLiteStore.Pin(new DocumentProcessingEvent { Document = Fake.Document, Timestamp = DateTime.Now }, Fake.User);

                Console.WriteLine($"Pinned new visit @ {DateTime.Now}");

                timer.Interval = random.Next(1000, 10000);
                timer.Start();
            };
            timer.Start();
        }
    }
}
