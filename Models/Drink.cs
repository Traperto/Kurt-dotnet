using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class Drink
    {
        public Drink()
        {
            Proceed = new HashSet<Proceed>();
            RefillContainment = new HashSet<RefillContainment>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Proceed> Proceed { get; set; }
        public virtual ICollection<RefillContainment> RefillContainment { get; set; }
    }
}
