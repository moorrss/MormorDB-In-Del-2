using Microsoft.EntityFrameworkCore;
using MormorDB.Entities;

namespace MormorDB.Data;

public class MormorDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().
        HasKey(sp => new { sp.SupplierId, sp.ProductId });
    }
}

