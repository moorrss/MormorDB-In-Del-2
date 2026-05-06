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

    if (context.Customers.Any()) return;

        var customers = new List<Customer>
    {
        new Customer
    {
        StoreName = "ICA Maxi Helsingborg",
        Phone = "042123456",
        Email = "ica@maxi.se",
        ContactPerson = "Lars Nilsson",
        DeliveryAddress = "Storgatan 1, Helsingborg",
        InvoiceAddress = "Box 100, Helsingborg"
    },
        new Customer
    {
        StoreName = "Coop Centrum",
        Phone = "042654321",
        Email = "coop@centrum.se",
        ContactPerson = "Anna Johansson",
        DeliveryAddress = "Köpmansgatan 5, Helsingborg",
        InvoiceAddress = "Box 200, Helsingborg"
    }
    };
        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

    if (context.Orders.Any()) return;

        var orders = new List<Order>
    {
        new Order
    {
        OrderNumber = "ORD-001",
        OrderDate = new DateTime(2025, 2, 20),
        CustomerId = customers[0].CustomerId,
        OrderLines = new List<OrderItem>
        {
            new OrderItem { ProductId = mjol.ProductId, Quantity = 10, Price = mjol.Price },
            new OrderItem { ProductId = socker.ProductId, Quantity = 5, Price = socker.Price }
        }
    },
        new Order
    {
        OrderNumber = "ORD-002",
        OrderDate = new DateTime(2025, 2, 21),
        CustomerId = customers[1].CustomerId,
        OrderLines = new List<OrderItem>
    {
            new OrderItem { ProductId = mjol.ProductId, Quantity = 20, Price = mjol.Price }
    }
    }
    };
        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

    }
}
