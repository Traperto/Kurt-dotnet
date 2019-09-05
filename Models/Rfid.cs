using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ColaTerminal.Models
{
    public class Rfid
    {
        public uint id { get; set; }
        public string rfId { get; set; }
        public uint userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
    }
}