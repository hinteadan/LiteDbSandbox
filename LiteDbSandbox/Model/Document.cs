namespace LiteDbSandbox.Model
{
    public class Document
    {
        public string Name { get; set; }
        public ulong SizeInBytes { get; set; }
        public uint NumberOfPages { get; set; }
        public EtiProcessingResult ProcessResult { get; set; }
    }
}
