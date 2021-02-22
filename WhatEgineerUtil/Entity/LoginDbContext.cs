using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using WhatEgineerSqlite.Entity.ShopInfo;

namespace SQLite.CodeFirst.NetCore.Console
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(string ConnectionString)
            : base(ConnectionString)
        {
            var strBuild = new SQLiteConnectionStringBuilder(ConnectionString);
            var path = Path.GetDirectoryName(strBuild.DataSource);
            Directory.CreateDirectory(path);
            var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            Configure();
        }

        public LoginDbContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DbContext>(null);
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new LoginDbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
        //public DbSet<PictureResourceInfo> PictureRes { get; set; }

    }
}