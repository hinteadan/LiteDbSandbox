using System;

namespace LiteDbSandbox.Model
{
    public class EtiProcessingResult
    {
        public bool HasSucceeded { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public TimeSpan Duration { get; set; }
        public uint NumberOfTablesFound { get; set; }
    }
}