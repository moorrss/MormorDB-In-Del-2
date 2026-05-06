using MormorDB.Entities;

namespace MormorDB.Data;

public class SeedData
{
    public static async Task Initialize(MormorDbContext context)
    {
        if (context.Suppliers.Any()) return;

        var suppliers = new List<Supplier>
        {
            new Supplier { Name = "Arla", Address = "Mjölkgatan 1", ContactPerson = "Anna Svensson", Phone = "0701234567", Email = "arla@arla.se" },
            new Supplier { Name = "Fazer", Address = "Brödbyn 2", ContactPerson = "Erik Karlsson", Phone = "0709876543", Email = "fazer@fazer.se" }
        };
        await context.Suppliers.AddRangeAsync(suppliers);
        await context.SaveChangesAsync();

        if (context.Products.Any()) return;
        var mjol = new Product { ArticleNumber = "ART001", ProductName = "Vetemjöl" };
        var socker = new Product { ArticleNumber = "ART002", ProductName = "Socker" };

        await context.Products.AddRangeAsync(mjol, socker);
        await context.SaveChangesAsync();

        var supplierProducts = new List<SupplierProduct>
    {
        new SupplierProduct { SupplierId = suppliers[0].SupplierId, ProductId = mjol.ProductId, PriceKg = 12.50m },
        new SupplierProduct { SupplierId = suppliers[1].SupplierId, ProductId = mjol.ProductId, PriceKg = 11.00m },
        new SupplierProduct { SupplierId = suppliers[0].SupplierId, ProductId = socker.ProductId, PriceKg = 8.00m }
    };
        await context.SupplierProducts.AddRangeAsync(supplierProducts);
        await context.SaveChangesAsync();

    }
}
