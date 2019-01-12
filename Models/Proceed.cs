using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class Proceed
    {
        public uint Id { get; set; }
        public uint? UserId { get; set; }
        public uint? DrinkId { get; set; }
        public DateTime? Date { get; set; }

        public virtual Drink Drink { get; set; }
        public virtual User User { get; set; }
    }
}
