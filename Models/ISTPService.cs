using System.Threading.Tasks;

namespace SuperPayments.Models
{
    public interface ISTPService
    {
        Task<TransactionResult> ProcessTransactionAsync(Transaction transaction);
    }
}
