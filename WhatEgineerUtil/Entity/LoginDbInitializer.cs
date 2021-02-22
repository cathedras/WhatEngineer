using System.Data.Entity;
using SQLite.CodeFirst.NetCore.Console.Entity;
using WhatEgineerSqlite.Entity.ShopInfo;

namespace SQLite.CodeFirst.NetCore.Console
{
    public class LoginDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<LoginDbContext>
    {
        public LoginDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, typeof(CustomHistory))
        { }

        protected override void Seed(LoginDbContext context)
        {
            // Here you can seed your core data if you have any.
            //context.Set<PictureResourceInfo>().Add(new PictureResourceInfo() { DiscribeName="hhhhhhh",Id=0,SavePath="pict.jpg"});
        }
    }
}