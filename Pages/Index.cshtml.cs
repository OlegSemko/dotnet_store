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
    public List<Product> Products { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int? SearchId { get; set; }

    [BindProperty]
    public string ProductName { get; set; }

    [BindProperty]
    public int ProductQuantity { get; set; }

    [BindProperty]
    public string ProductLocation { get; set; }
    public string Message { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchProduct { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? SearchQuantity { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchLocation { get; set; }

    [BindProperty]
    public string SellProductIds { get; set; }

    [BindProperty]
    public string QuantitiesSold { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Products.AsQueryable();

        if (SearchId.HasValue)
        {
            query = query.Where(p => p.Id == SearchId.Value);
        }

        if (!string.IsNullOrWhiteSpace(SearchProduct))
        {
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{SearchProduct}%"));
        }

        if (SearchQuantity.HasValue)
        {
            query = query.Where(p => p.Quantity == SearchQuantity.Value);
        }

        if (!string.IsNullOrWhiteSpace(SearchLocation))
        {
            query = query.Where(p => EF.Functions.Like(p.Location, $"%{SearchLocation}%"));
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
        }

        LoadProducts();

        return RedirectToPage();
    }

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
        
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUpdateLocationAsync(int id, string location)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        product.Location = location;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRecordSaleAsync()
    {
        var productIds = SellProductIds.Split(',').Select(int.Parse).ToList();
        var quantities = QuantitiesSold.Split(',').Select(int.Parse).ToList();

        if (productIds.Count != quantities.Count)
        {
            ModelState.AddModelError("", "Mismatched product IDs and quantities.");
            return RedirectToPage();
        }

        for (int i = 0; i < productIds.Count; i++)
        {
            int productId = productIds[i];
            int quantitySold = quantities[i];

            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                if (product.Quantity < quantitySold)
                {
                    return RedirectToPage();
                }
                product.Quantity = (product.Quantity ?? 0) - quantitySold;
            }
        }

        var sale = new Sale
        {
            SellProductIds = SellProductIds,
            QuantitiesSold = QuantitiesSold,
            SoldAt = DateTime.Now
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        LoadProducts();

        return RedirectToPage();
    }

    private void LoadProducts()
    {
        Products = _context.Products.ToList();
    }
}
