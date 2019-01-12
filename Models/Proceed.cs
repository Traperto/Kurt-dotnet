using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ColaTerminal.Models
{
    public partial class Proceed
    {
        public Proceed()
        {

        }

        public uint Id { get; set; }

        //[ForeignKey("User")]
        public uint? UserId { get; set; }
        [ForeignKey("Drink")]
        public uint? DrinkId { get; set; }
        public DateTime? Date { get; set; }

        [InverseProperty("Proceed")]
        public Drink Drink { get; set; }

        [IgnoreDataMember]
        // [InverseProperty("Proceed")]
        public virtual User User { get; set; }
    }
}
