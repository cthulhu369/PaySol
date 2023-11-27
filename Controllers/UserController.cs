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
        using (var db = new ApplicationDbContext()) {
            db.Add(model);
            db.SaveChanges();
        }

        return View(model); // Return the form with validation messages
    }
}
