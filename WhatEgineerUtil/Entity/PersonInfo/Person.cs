using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLite.CodeFirst.NetCore.Console.Entity
{
    /// <summary>
    /// 人员管理的基类，人员的基础信息
    /// </summary>
    public abstract class Person : IEntity
    {
        [MaxLength(50)]
        [Collate(CollationFunction.NoCase)]
        public string Name { get; set; }


        [MaxLength(200)]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [MaxLength(100)]
        [Required]
        public string Country { get; set; }

        [Key]
        [MaxLength(20)]
        [Column(Order = 1)]
        public string Phone { get; set; }

        [Key]
        [MaxLength(50)]
        [Column(Order = 2)]
        public string Email { get; set; }

        [Key]
        [MaxLength(50)]
        [Column(Order = 3)]
        public string RegisterType { get; set; }
        [Key]
        [MaxLength(50)]
        [Column(Order = 4)]
        public string RoleID { get; set; }
        [Key]
        [MaxLength(50)]
        [Column(Order = 5)]
        public string ResourceID { get; set; }


        [MaxLength(1)]
        public char State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [SqlDefaultValue(DefaultValue = "DATETIME('now')")]
        public DateTime CreatedUtc { get; set; }
        
    }
}
