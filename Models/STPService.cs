using SuperPayments.Models;


namespace SuperPayments.Services
{
    public class STPService : ISTPService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRuleEngineService _ruleEngine;

        public STPService(ApplicationDbContext context, IRuleEngineService ruleEngine)
        {
            _context = context;
            _ruleEngine = ruleEngine;
        }

        public async Task<TransactionResult> ProcessTransactionAsync(Transaction transaction)
        {
            if (_ruleEngine.ValidateTransaction(transaction))
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return new TransactionResult { Success = true };
            }

            return new TransactionResult { Success = false };
        }
    }
}
