using Microsoft.AspNetCore.Mvc;
using SuperPayments.Models;


public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
        
        // Ensure the database is created
        _context.Database.EnsureCreated();

    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegisterUser(User model)
    {
        try
        {
            model.PayKey = GeneratePayKey();
            _context.Add(model);
            _context.SaveChanges();

            ViewBag.Message = $"User registered successfully. ID: {model.Id}, Email: {model.Email}, Name: {model.Name}, PayKey: {model.PayKey}";
            Console.WriteLine(ViewBag.Message);
            return View("Register"); // Assuming "Register" is your view name
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            Console.WriteLine(ViewBag.ErrorMessage);
            return View("Register");
        }
    }
    private string GeneratePayKey()
    {
        var random = new Random();
        return new string(Enumerable.Repeat("0123456789", 10)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    // Inside the registration method



}
