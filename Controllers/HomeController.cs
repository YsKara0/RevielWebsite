using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using reviel.Models;
using reviel.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace reviel.Controllers;

public class HomeController : Controller
{    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(); 
    }

    public async Task<IActionResult> Products(string? productType)
    {
        ViewData["ProductTypes"] = Enum.GetValues(typeof(ProductType))
                                        .Cast<ProductType>()
                                        .Select(pt => pt.ToString())
                                        .ToList();

        var productsQuery = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(productType))
        {
            if (Enum.TryParse<ProductType>(productType, true, out var typeEnum))
            {
                productsQuery = productsQuery.Where(p => p.Type == typeEnum);
                ViewData["SelectedProductType"] = productType;
            }
        }

        var products = await productsQuery.ToListAsync();
        return View(products);
    }    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> Researches()
    {
        var researches = await _context.Researches
            .OrderByDescending(r => r.PublicationDate)
            .ToListAsync();
        
        return View(researches);
    }    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
