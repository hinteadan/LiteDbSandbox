using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDbSandbox.SQLiteStore
{
    class SQLiteStore
    {
        public static void Pin(Model.DocumentProcessingEvent document, Model.User user)
        {
            using (var db = new TabstractAnalyticsEntities())
            {
                var dbUser = db.Users.Where(u => u.Username.ToLower() == user.Username.ToLower()).SingleOrDefault() ?? Mapper.Map(user);

                var dbDocument = Mapper.Map(document);
                dbDocument.Users = dbUser;

                db.DocumentProcessingEvent.Add(dbDocument);
                db.SaveChanges();
            }
        }
    }
}
