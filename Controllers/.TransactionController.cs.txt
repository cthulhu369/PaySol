using SuperPayments.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuperPayments.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(string senderPayKey, int receiverId, decimal amount)
        {
            // Validate sender based on PayKey
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.PayKey == senderPayKey);
            if (sender == null)
            {
                return Json(new { success = false, message = "Invalid sender paykey." });
            }

            // Validate receiver existence
            var receiver = await _context.Users.FindAsync(receiverId);
            if (receiver == null)
            {
                return Json(new { success = false, message = "Receiver not found." });
            }

            // Create and save transaction
            var transaction = new Transaction
            {
                Amount = amount,
                Date = DateTime.Now,
                SenderId = sender.Id,
                ReceiverId = receiver.Id
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Payment successful." });
        }
    }

}
