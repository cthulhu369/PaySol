using System;

namespace SuperPayments.Models
{
    public class Transaction {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Vendor { get; set; }

        // Foreign keys for sender and receiver
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? SenderPayKey { get; set; }

        // Navigation properties
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }

}
