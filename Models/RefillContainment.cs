using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class RefillContainment
    {
        public uint RefillId { get; set; }
        public uint DrinkId { get; set; }
        public int? Quantity { get; set; }

        public virtual Drink Drink { get; set; }
        public virtual Refill Refill { get; set; }
    }
}
