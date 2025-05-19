using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using reviel.Data;
using reviel.Models;

namespace reviel.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/AddProduct
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Admin/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct([Bind("Name,Type,Description,ImagePath,Benefits,KeyIngredients")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewProducts));
            }
            return View(product);
        }

        // GET: Admin/ViewProducts
        public async Task<IActionResult> ViewProducts()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, [Bind("Id,Name,Type,Description,ImagePath,Benefits,KeyIngredients")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewProducts));
            }
            return View(product);
        }

        // GET: Admin/DeleteProduct/5
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewProducts));
        }

        // GET: Admin/ViewResearches
        public async Task<IActionResult> ViewResearches()
        {
            var researches = await _context.Researches.OrderByDescending(r => r.PublicationDate).ToListAsync();
            return View(researches);
        }

        // GET: Admin/AddResearch
        public IActionResult AddResearch()
        {
            // Yeni bir araştırma nesnesi oluştur ve bugünkü tarihi otomatik ata
            var research = new Research
            {
                PublicationDate = DateTime.Today
            };            return View(research);
        }
        
        // POST: Admin/AddResearch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResearch([Bind("Id,Title,Summary,PublicationDate,Author,PdfUrl")] Research research)
        {
            if (ModelState.IsValid)
            {
                _context.Add(research);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewResearches));
            }
            return View(research);
        }

        // GET: Admin/EditResearch/5
        public async Task<IActionResult> EditResearch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches.FindAsync(id);
            if (research == null)
            {
                return NotFound();
            }            return View(research);
        }
        
        // POST: Admin/EditResearch/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResearch(int id, [Bind("Id,Title,Summary,PublicationDate,Author,PdfUrl")] Research research)
        {
            if (id != research.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(research);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchExists(research.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewResearches));
            }
            return View(research);
        }

        // GET: Admin/DeleteResearch/5
        public async Task<IActionResult> DeleteResearch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (research == null)
            {
                return NotFound();
            }

            return View(research);
        }

        // POST: Admin/DeleteResearch/5
        [HttpPost, ActionName("DeleteResearch")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteResearchConfirmed(int id)
        {
            var research = await _context.Researches.FindAsync(id);
            if (research != null)
            {
                _context.Researches.Remove(research);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewResearches));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private bool ResearchExists(int id)
        {
            return _context.Researches.Any(e => e.Id == id);
        }
    }
}
