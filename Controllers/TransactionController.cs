using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperPayments.Models;
using SuperPayments.Services;

namespace SuperPayments.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ISTPService _stpService;

        public TransactionController(ISTPService stpService)
        {
            _stpService = stpService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTransaction(Transaction transaction)
        {
            var result = await _stpService.ProcessTransactionAsync(transaction);
            return Json(result); // Simplified for demonstration
        }
    }
}
