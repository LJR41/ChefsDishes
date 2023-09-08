using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsDishes.Models;

namespace ChefsDishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public ChefController(ILogger<ChefController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }
    [HttpGet("chef/new")]
    public ViewResult NewChef()
    {
        return View();
    }
    [HttpGet("chef/all")]
    public ViewResult AllChef()
    {
        List<Chef> Chefs = _context.Chef.OrderByDescending(c => c.CreatedAt).ToList();
        return View("AllChef", Chefs);
    }
    [HttpPost("chef/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if(!ModelState.IsValid)
        {
            return View("NewChef");
        }
        _context.Add(newChef);
        _context.SaveChanges();
        return RedirectToAction("AllChef");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
