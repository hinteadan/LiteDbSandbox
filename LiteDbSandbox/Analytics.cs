using LiteDB;
using LiteDbSandbox.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace LiteDbSandbox
{
    class Analytics
    {
        private static readonly FileInfo dbFile = new FileInfo("LiteDbSandboxDatabase.db");

        private static readonly string collectionNameForDocProcessingEvents = "documentProcessingEvents";
        private static readonly string collectionNameForUsers = "users";

        private readonly User user;

        public Analytics(User user)
        {
            this.user = EnsureUser(user);

            LiteDbMapping.Go();
        }

        private User EnsureUser(User user)
        {
            using (var db = new LiteDatabase(dbFile.FullName))
            {
                var existingUser = db
                    .GetCollection<User>(collectionNameForUsers)
                    .FindOne(x => x.Username == user.Username);

                if (existingUser != null)
                {
                    return existingUser;
                }

                db
                    .GetCollection<User>(collectionNameForUsers)
                    .Insert(user);

                return user;
            }
        }

        public static Analytics For(User user)
        {
            return new Analytics(user);
        }

        public Analytics Pin(Document document)
        {
            using (var db = new LiteDatabase(dbFile.FullName))
            {

                db
                    .GetCollection<DocumentProcessingEvent>(collectionNameForDocProcessingEvents)
                    .Insert(new DocumentProcessingEvent
                    {
                        Document = document,
                        User = user,
                        Timestamp = DateTime.Now
                    });
            }

            return this;
        }

        public IEnumerable<DocumentProcessingEvent> List()
        {
            using (var db = new LiteDatabase(dbFile.FullName))
            {
                return db
                    .GetCollection<DocumentProcessingEvent>(collectionNameForDocProcessingEvents)
                    .Include(x => x.User)
                    .Find(x => x.Document.ProcessResult.HasSucceeded == false);
            }
        }
    }
}