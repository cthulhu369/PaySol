using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperPayments.Models;

namespace SuperPayments.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult RegisterUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegisterUser(User model) {
        try {
            using (var db = new ApplicationDbContext()) {
                model.PayKey = GeneratePayKey();
                db.Add(model);
                db.SaveChanges();
            }

            ViewBag.Message = $"User registered successfully. ID: {model.Id}, Email: {model.Email}, Name: {model.Name}, PayKey: {model.PayKey}";
            Console.WriteLine(ViewBag.Message);
            return View("RegisterUser"); // Assuming "Register" is your view name
        } catch (Exception ex) {
            ViewBag.ErrorMessage = ex.Message;
            Console.WriteLine(ViewBag.ErrorMessage);
            return View("RegisterUser");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    private string GeneratePayKey()
    {
        var random = new Random();
        return new string(Enumerable.Repeat("0123456789", 10)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    [HttpGet]
    public IActionResult MakePayment()
    {
        return View();
    }
    public async Task<IActionResult> MakePayment(string senderPayKey, int receiverId, decimal amount)
        {
            var _context = new ApplicationDbContext();
            _context.Database.EnsureCreated();
            // Validate sender based on PayKey
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.PayKey == senderPayKey);
            if (sender == null)
            {   
                ViewBag.Message = $"Invalid sender paykey.";
                Console.WriteLine(ViewBag.Message);
                return View("MakePayment");
            }

            // Validate receiver existence
            var receiver = await _context.Users.FindAsync(receiverId);
            if (receiver == null)
            {
                ViewBag.Message = $"Receiver not found.";
                Console.WriteLine(ViewBag.Message);
                return View("MakePayment");
            }
            // Validate field datatypes
            if (amount <= 0)
            {
                ViewBag.Message = $"Invalid amount.";
                Console.WriteLine(ViewBag.Message);
                return View("MakePayment");
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
            ViewBag.Message = $"Payment successful. Transaction ID: {transaction.Id}";

            return View("MakePayment");
        }
        public IActionResult GeneralLedger()
        {
            var _context = new ApplicationDbContext();
            _context.Database.EnsureCreated();
            var transactions = _context.Transactions.ToList(); // Assuming _context is your DbContext
            return View(transactions);
        }



}
