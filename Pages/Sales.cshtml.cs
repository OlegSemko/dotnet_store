using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTestApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTestApp.Data;

namespace MyTestApp.Pages
{
    public class SalesModel : PageModel
    {
        private readonly AppDbContext _context;

        public SalesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<SaleViewModel> SaleViewModels { get; set; }

        public async Task OnGetAsync()
        {
            var sales = await _context.Sales.ToListAsync();
            var allProducts = await _context.Products.ToListAsync();

            SaleViewModels = new List<SaleViewModel>();

            foreach (var sale in sales)
            {
                var productIds = sale.SellProductIds.Split(',').Select(id => int.TryParse(id.Trim(), out var pid) ? pid : (int?)null).Where(id => id.HasValue).Select(id => id.Value).ToList();
                var quantities = sale.QuantitiesSold.Split(',').Select(q => int.TryParse(q.Trim(), out var qty) ? qty : (int?)null).Where(q => q.HasValue).Select(q => q.Value).ToList();

                for (int i = 0; i < productIds.Count && i < quantities.Count; i++)
                {
                    var product = allProducts.FirstOrDefault(p => p.Id == productIds[i]);
                    if (product != null)
                    {
                        SaleViewModels.Add(new SaleViewModel
                        {
                            ProductName = product.Name,
                            QuantitySold = quantities[i],
                            SoldAt = sale.SoldAt,
                            Id = sale.Id
                        });
                    }
                }
            }
        }

        public class SaleViewModel
        {
            public string ProductName { get; set; }
            public int QuantitySold { get; set; }
            public System.DateTime SoldAt { get; set; }
            public int Id { get; set; }
        }
    }
}
