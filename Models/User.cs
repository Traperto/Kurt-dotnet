using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class User
    {
        public User()
        {
            BalanceTransaction = new HashSet<BalanceTransaction>();
            Proceed = new HashSet<Proceed>();
            Refill = new HashSet<Refill>();
        }

        public uint Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? Balance { get; set; }
        public int? RfId { get; set; }
        public string Password { get; set; }

        public virtual ICollection<BalanceTransaction> BalanceTransaction { get; set; }
        public virtual ICollection<Proceed> Proceed { get; set; }
        public virtual ICollection<Refill> Refill { get; set; }
    }
}
