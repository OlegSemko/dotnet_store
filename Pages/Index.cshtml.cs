using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTestApp.Data;
using MyTestApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyTestApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    // public bool ShowCreateBlock { get; set; }
    public List<Product> Products { get; set; } = new();

    [BindProperty]
    public string ProductName { get; set; }
    [BindProperty]
    public int ProductQuantity { get; set; }
    [BindProperty]
    public string ProductLocation { get; set; }
    public string Message { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchProduct { get; set; }



    public async Task OnGetAsync()
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchProduct))
        {
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{SearchProduct}%"));
        }

        Products = await query.ToListAsync();
    }

    public IActionResult OnPost()
    {
        if (!string.IsNullOrWhiteSpace(ProductName))
        {
            var product = new Product { Name = ProductName, Quantity = ProductQuantity, Location = ProductLocation };
            _context.Products.Add(product);
            _context.SaveChanges();
            Message = "Product added successfully!";
            // ShowCreateBlock = false;
        }

        LoadProducts();

        return RedirectToPage();
    }

    // public IActionResult OnPostToggle()
    // {
    //     ShowCreateBlock = !ShowCreateBlock;
    //     Products = _context.Products.ToList();
    //     return Page();
    // }

    public IActionResult OnPostDelete(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        // Reload products after deletion
        LoadProducts();
        
        return Page();
    }

    private void LoadProducts()
    {
        Products = _context.Products.ToList();
    }
}
