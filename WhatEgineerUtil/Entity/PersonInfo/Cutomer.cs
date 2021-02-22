using System.ComponentModel.DataAnnotations.Schema;

namespace SQLite.CodeFirst.NetCore.Console.Entity
{
    [Table("Cutomer")]
    public class Cutomer : Person
    {
        [Index] // Automatically named 'IX_TeamPlayer_Number'
        [Index("CustomerIndex", Order = 1, IsUnique = true)]
        public string Number { get; set; }

        //// The index attribute must be placed on the FK not on the navigation property (team).
        //[Index("IX_TeamPlayer_NumberPerTeam", Order = 2, IsUnique = true)]
        //public int TeamId { get; set; }

        //// Its not possible to set an index on this property. Use the FK property (teamId).
        //public virtual Team Team { get; set; }
        

        public virtual Cutomer Mentor { get; set; }
    }
}