using SQLite.CodeFirst;
using SQLite.CodeFirst.NetCore.Console.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WhatEgineerSqlite.Entity.Source
{
    [Table("Role")]
    public class Role : IEntity
    {
        [DataType(DataType.Text)]
        public string RoleID { set; get; }
        [DataType(DataType.Text)]
        public string RoleName { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('now')")]
        public DateTime CreateTime { set; get; }
        public Resource Resource { set; get; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
