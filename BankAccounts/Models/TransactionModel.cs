using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    public class Transaction
    {
        [Key] // denotes PK, not needed if named ModelNameId
        public int TransactId { get; set; }
        public Decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User Customer { get; set; }
    
    }
}