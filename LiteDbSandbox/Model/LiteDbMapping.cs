using LiteDB;

namespace LiteDbSandbox.Model
{
    public static class LiteDbMapping
    {
        private static bool isConfigured = false;

        public static void Go()
        {
            if (isConfigured)
            {
                return;
            }

            BsonMapper.Global.Entity<User>()
                .Id(x => x.Username)
                .Index(x => x.Email);

            BsonMapper.Global.Entity<Document>()
                .Index(x => x.NumberOfPages)
                .Index(x => x.ProcessResult);

            BsonMapper.Global.Entity<DocumentProcessingEvent>()
                .DbRef(x => x.User, "users")
                .Index(x => x.Document)
                .Index(x => x.User);

            isConfigured = true;
        }
    }
}
