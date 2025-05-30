using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTestApp.Data;
using MyTestApp.Models;

namespace MyTestApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; }

    [BindProperty]
    public string ProductName { get; set; }
    public string Message { get; set; }

    public void OnGet()
    {
        Products = _context.Products.ToList();
    }

    public void OnPost()
    {
        if (!string.IsNullOrWhiteSpace(ProductName))
        {
            var product = new Product { Name = ProductName };
            _context.Products.Add(product);
            _context.SaveChanges();
            Message = "Product added successfully!";
        }

        Products = _context.Products.ToList();
    }
}
