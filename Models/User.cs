using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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
        public string Password { get; set; }

        [IgnoreDataMember] public ICollection<BalanceTransaction> BalanceTransaction { get; set; }
        [InverseProperty("User")] public ICollection<Proceed> Proceed { get; set; }
        public ICollection<Refill> Refill { get; set; }
    }
}