namespace LiteDbSandbox.Model
{
    public class Document
    {
        public string Name { get; set; }
        public long SizeInBytes { get; set; }
        public int NumberOfPages { get; set; }
        public EtiProcessingResult ProcessResult { get; set; }
    }
}
