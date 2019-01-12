using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class Refill
    {
        public Refill()
        {
            RefillContainment = new HashSet<RefillContainment>();
        }

        public uint Id { get; set; }
        public DateTime? Date { get; set; }
        public uint? UserId { get; set; }
        public double? Price { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<RefillContainment> RefillContainment { get; set; }
    }
}
