using SuperPayments.Models;

namespace SuperPayments.Models {
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Navigation properties
        public List<Transaction> SentTransactions { get; set; }
        public List<Transaction> ReceivedTransactions { get; set; }
    }
}