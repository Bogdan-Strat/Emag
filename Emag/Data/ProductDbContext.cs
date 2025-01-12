using Emag.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Emag.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.AddInboxStateEntity();
        //    modelBuilder.AddOutboxMessageEntity();
        //    modelBuilder.AddOutboxStateEntity();
        //}
    }
}
