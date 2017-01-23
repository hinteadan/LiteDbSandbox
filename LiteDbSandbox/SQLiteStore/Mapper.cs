using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDbSandbox.SQLiteStore
{
    static class Mapper
    {
        public static DocumentProcessingEvent Map(Model.DocumentProcessingEvent docEvent)
        {
            return new DocumentProcessingEvent
            {
                Id = Guid.NewGuid(),
                Timestamp = docEvent.Timestamp,
                DocumentName = docEvent.Document.Name,
                DocumentNumberOfPages = (int)docEvent.Document.NumberOfPages,
                DocumentSizeInBytes = (long)docEvent.Document.SizeInBytes,
                ProcessingResultDuration = docEvent.Document.ProcessResult.Duration.Ticks,
                ProcessingResultErrorDetails = docEvent.Document.ProcessResult.ErrorDetails,
                ProcessingResultErrorMessage = docEvent.Document.ProcessResult.ErrorMessage,
                ProcessingResultHasSucceeded = docEvent.Document.ProcessResult.HasSucceeded,
                ProcessingResultNumberOfTablesFound = (int)docEvent.Document.ProcessResult.NumberOfTablesFound
            };
        }

        public static Users Map(Model.User user)
        {
            return new Users
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
