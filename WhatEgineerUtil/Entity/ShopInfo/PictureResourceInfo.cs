using SQLite.CodeFirst;
using SQLite.CodeFirst.NetCore.Console.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhatEgineerSqlite.Entity.ShopInfo
{
    [Table("PictureResource")]
    public class PictureResourceInfo:IEntity
    {
        [DataType(DataType.Text)]
        public string DispPath { get; set; }
        [DataType(DataType.Text)]
        public string SavePath { get; set; }
        [DataType(DataType.Text)]
        public string Descrition { get; set; }
        [DataType(DataType.Text)]
        public string DetailDescrition { get; set; }
        [DataType(DataType.Text)]
        public string CreateBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('now')")]
        public DateTime CreateTime { get; set; }
    }
}
