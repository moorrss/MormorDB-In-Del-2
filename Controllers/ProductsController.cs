using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDB.Data;
using MormorDB.Entities;

namespace MormorDB.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(MormorDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await context.Products
        .Include(p => p.SupplierProducts)
        .ThenInclude(sp => sp.Supplier)
        .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await context.Products
        .Include(p => p.SupplierProducts)
        .ThenInclude(sp => sp.Supplier)
        .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    [HttpPatch("{id}/price")]
    public async Task<ActionResult> UpdateProductPrice(int id, decimal price)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound();

        product.Price = price;
        await context.SaveChangesAsync();
        return Ok(product);
    }
    [HttpGet("{id}/customer")]
    public async Task<ActionResult> GetCustomersByProductId(int id)
    {
        var product = await context.Products
        .Include(p => p.OrderItems)
        .ThenInclude(oi => oi.Order)
        .ThenInclude(o => o.Customer)
        .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product is null) return NotFound();

        var customers = product.OrderItems.Select(oi => oi.Order.Customer).Distinct().ToList();
        return Ok(customers);
    }
}
