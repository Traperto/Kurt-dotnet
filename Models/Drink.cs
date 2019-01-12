using System.Collections.Generic;
using System.Runtime.Serialization;

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
        public double Price { get; set; }

        [IgnoreDataMember]
        public ICollection<Proceed> Proceed { get; set; }
        public ICollection<RefillContainment> RefillContainment { get; set; }
    }
}
