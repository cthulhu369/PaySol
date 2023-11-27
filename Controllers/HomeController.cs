using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
                db.Add(model);
                db.SaveChanges();
            }

            ViewBag.Message = $"User registered successfully. ID: {model.Id}, Email: {model.Email}, Name: {model.Name}";
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
}
