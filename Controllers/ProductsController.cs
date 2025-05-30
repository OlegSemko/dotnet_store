// using Microsoft.AspNetCore.Mvc;
// using YourApp.Models;
// using YourApp.Data;

// public class ProductsController : Controller
// {
//     private readonly AppDbContext _context;

//     public ProductsController(AppDbContext context)
//     {
//         _context = context;
//     }

//     [HttpGet]
//     public IActionResult Index()
//     {
//         return View();
//     }

//     [HttpPost]
//     public IActionResult AddProduct(string name)
//     {
//         if (!string.IsNullOrWhiteSpace(name))
//         {
//             var product = new Product { Name = name };
//             _context.Products.Add(product);
//             _context.SaveChanges();
//         }

//         return RedirectToAction("Index");
//     }
// }
