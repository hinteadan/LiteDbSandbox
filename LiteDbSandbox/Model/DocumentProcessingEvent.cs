using LiteDB;
using System;

namespace LiteDbSandbox.Model
{
    public class DocumentProcessingEvent
    {
        public ObjectId Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Document Document { get; set; }
        public User User { get; set; }
    }
}
