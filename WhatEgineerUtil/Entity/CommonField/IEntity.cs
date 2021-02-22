using NHibernate.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLite.CodeFirst.NetCore.Console.Entity
{
    public abstract class IEntity
    {
        [Key]
        [Column(Order = 0)]
        public Guid GId { get; set; }
    }
}
