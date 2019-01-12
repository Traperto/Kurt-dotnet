using System;
using System.Collections.Generic;

namespace ColaTerminal.Models
{
    public partial class BalanceTransaction
    {
        public uint Id { get; set; }
        public uint? UserId { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }
    }
}
