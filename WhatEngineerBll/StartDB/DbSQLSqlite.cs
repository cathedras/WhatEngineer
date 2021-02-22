using SQLite.CodeFirst.NetCore.Console;
using SQLite.CodeFirst.NetCore.Console.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Text;
using WhatEgineerSqlite.Entity.Source;

namespace WhatEngineerBll.StartDB
{
    public class DbSQLSqlite<TDbContext> where TDbContext : DbContext
    {
        public static DbContext GetDbContext(string connection)
        {
            TDbContext context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), connection, false);
            return context;
        }

        private static void DisplaySeededData(DbContext context)
        {

        }
    }
}
