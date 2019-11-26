using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ColaTerminal.Models
{
    public class Rfid
    {
        [Key]
        public string rfId { get; set; }

        [Key]
        public uint userId { get; set; }

        [ForeignKey("userId")]
        public User User { get; set; }
    }
}