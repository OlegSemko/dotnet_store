using Microsoft.EntityFrameworkCore;
using MyTestApp.Models;

namespace MyTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
    }
}
