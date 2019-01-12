using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ColaTerminal.Models
{
    public partial class Proceed
    {
        public uint Id { get; set; }
        public uint? UserId { get; set; }
        [ForeignKey("Drink")] public uint? DrinkId { get; set; }
        public DateTime? Date { get; set; }
        public double Price { get; set; }

        [InverseProperty("Proceed")] public Drink Drink { get; set; }
        [IgnoreDataMember] public virtual User User { get; set; }
    }
}