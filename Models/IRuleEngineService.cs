using SuperPayments.Models;

namespace SuperPayments.Services
{
    public interface IRuleEngineService
    {
        bool ValidateTransaction(Transaction transaction);
    }
}
