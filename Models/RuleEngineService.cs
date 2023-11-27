using SuperPayments.Models;

namespace SuperPayments.Services
{
    public class RuleEngineService : IRuleEngineService
    {
        public bool ValidateTransaction(Transaction transaction)
        {
            // Implement rule validation logic here
            // For now, let's assume all transactions are valid
            return true;
        }
    }
}
