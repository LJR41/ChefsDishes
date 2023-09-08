using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public DishController(ILogger<DishController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }

    [HttpGet("dish/new")]
    public ViewResult NewDish()
    {
        List<Chef> Chefs = _context.Chef.OrderByDescending(c => c.CreatedAt).ToList();
        ViewBag.Chefs = Chefs;
        return View("NewDish");
    }
    [HttpGet("dish/all")]
    public ViewResult AllDish()
    {
        List<Dish> Dishes = _context.Dish.Include(d => d.Chef).OrderByDescending(c => c.CreatedAt).ToList();
        return View("AllDish", Dishes);
    }
    [HttpPost("Dish/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return NewDish();
        }
        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction("AllDish");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
