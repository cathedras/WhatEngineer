using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;

namespace SQLite.CodeFirst.Test.IntegrationTests
{
    /// <summary>
    ///  Inherit this class to create a test which uses a SQLite in memory database. 
    ///  This class provides the necessary logic to run multiple tests again the in memory database in a row.
    /// </summary>
    public abstract class SqliteDbUtil<TDbContext> 
        where TDbContext : DbContext
    {
        private bool dbInitialized;

        protected DbConnection Connection { get; private set; }

        protected DbContext GetDbContext()
        {
            Initialize();
            TDbContext context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), Connection, false);
            if (!dbInitialized)
            {
                context.Database.Initialize(true);
                dbInitialized = true;
            }
            return context;
        }

        public void Initialize()
        {
           // Connection = new SQLiteConnection("data source=:memory:");
            Connection = new SQLiteConnection("data source=.\\Test.db;Pooling=true;FailIfMissing=false");

            // This is important! Else the in memory database will not work.
            Connection.Open();

            dbInitialized = false;
        }

        public void Cleanup()
        {
            Connection.Dispose();
        }
    }
}
