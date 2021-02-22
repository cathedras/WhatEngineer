using SQLite.CodeFirst;
using SQLite.CodeFirst.NetCore.Console.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhatEgineerSqlite.Entity.Source
{
    [Table("Resource")]
    public class Resource: IEntity
    {
        [DataType(DataType.Text)]
        public string ResourceID { get; set; }
        [DataType(DataType.Text)]
        public string ResourceName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('now')")]
        public DateTime CreateTime { set; get; }
        public Role Role { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
