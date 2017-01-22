using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace LiteDbSandbox
{
    class Reports
    {
        class GlobalDocumentReport
        {
            public ulong Count = 0;
            public ulong SuccessCount = 0;
            public ulong ErrorCount = 0;
            public ulong TotalTableCount = 0;
            public uint[] TableCounts = new uint[0];
            public uint[] NumberOfPages = new uint[0];
            public DateTime Timestamp = DateTime.MinValue;
        }

        public static void Generate()
        {
            GenerateGlobalDocumentReport();
        }

        private static void GenerateGlobalDocumentReport()
        {
            var existing = File.Exists("DocumentsReport.json") ?
                JsonConvert.DeserializeObject<GlobalDocumentReport>(File.ReadAllText("DocumentsReport.json")) :
                new GlobalDocumentReport();

            var report = Analytics
                .From(existing.Timestamp)
                .Select(d => new GlobalDocumentReport
                {
                    Count = 1,
                    SuccessCount = d.Document.ProcessResult.HasSucceeded ? (ulong)1 : 0,
                    ErrorCount = d.Document.ProcessResult.HasSucceeded ? (ulong)0 : 1,
                    TotalTableCount = d.Document.ProcessResult.NumberOfTablesFound,
                    TableCounts = new uint[] { d.Document.ProcessResult.NumberOfTablesFound },
                    NumberOfPages = new uint[] { d.Document.NumberOfPages },
                    Timestamp = d.Timestamp
                })
                .Concat(new GlobalDocumentReport[] { existing })
                .Aggregate((a, b) => new GlobalDocumentReport
                {
                    Count = a.Count + b.Count,
                    SuccessCount = a.SuccessCount + b.SuccessCount,
                    ErrorCount = a.ErrorCount + b.ErrorCount,
                    TotalTableCount = a.TotalTableCount + b.TotalTableCount,
                    TableCounts = a.TableCounts.Union(b.TableCounts).ToArray(),
                    NumberOfPages = a.NumberOfPages.Union(b.NumberOfPages).ToArray(),
                    Timestamp = a.Timestamp >= b.Timestamp ? a.Timestamp : b.Timestamp
                });

            File.WriteAllText("DocumentsReport.json", JsonConvert.SerializeObject(report, Formatting.Indented));
        }
    }
}
