using System.Data.Entity;
using SQLite.CodeFirst.NetCore.Console.Entity;
using WhatEgineerSqlite.Entity.ShopInfo;
using WhatEgineerSqlite.Entity.Source;

namespace SQLite.CodeFirst.NetCore.Console
{
    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureResourceEntity(modelBuilder);
            ConfigureRoleEntity(modelBuilder);
            ConfigureManagerEntity(modelBuilder);
            ConfigureResAndRoleEntity(modelBuilder);
            ConfigurePitureInfoEntity(modelBuilder);
        }

        private static void ConfigureResourceEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>().ToTable("Resource");
        }

        private static void ConfigureRoleEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
        }

        private static void ConfigureManagerEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cutomer>().ToTable("Customer");
        } 
        
        private static void ConfigureResAndRoleEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().ToTable("Manager");
        }
        
        private static void ConfigurePitureInfoEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PictureResourceInfo>().ToTable("PictureResource");//
        }
    }
}
