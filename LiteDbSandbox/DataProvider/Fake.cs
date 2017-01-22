using Bogus;
using LiteDbSandbox.Model;
using System;

namespace LiteDbSandbox.DataProvider
{
    static class Fake
    {
        private static Faker<User> user;
        private static Faker<EtiProcessingResult> etiResult;
        private static Faker<Document> document;

        static Fake()
        {
            user = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Username, f => f.Name.FindName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Username));

            etiResult = new Faker<EtiProcessingResult>()
                .StrictMode(true)
                .RuleFor(r => r.Duration, f => TimeSpan.FromSeconds(f.Random.Double(1, 120)))
                .RuleFor(r => r.HasSucceeded, f => f.Random.Bool())
                .RuleFor(r => r.ErrorMessage, (f, r) => r.HasSucceeded ? null : f.Lorem.Sentence())
                .RuleFor(r => r.ErrorDetails, (f, r) => r.HasSucceeded ? null : f.Lorem.Paragraph())
                .RuleFor(r => r.NumberOfTablesFound, f => f.Random.UShort(0, 50));

            document = new Faker<Document>()
                .StrictMode(true)
                .RuleFor(d => d.Name, f => f.System.FileName("pdf"))
                .RuleFor(d => d.NumberOfPages, f => (int)f.Random.UInt(0, 300))
                .RuleFor(d => d.ProcessResult, _ => etiResult.Generate())
                .RuleFor(d => d.SizeInBytes, f => f.Random.UInt(0, 1024 * 1024 * 100));
        }

        public static User User { get { return user.Generate(); } }
        public static Document Document { get { return document.Generate(); } }
    }
}
