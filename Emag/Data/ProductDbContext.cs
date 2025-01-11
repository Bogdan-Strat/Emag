using Emag.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emag.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Product> Products { get; set; }
    }
}
