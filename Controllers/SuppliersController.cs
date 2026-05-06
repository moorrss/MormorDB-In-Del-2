using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDB.Data;
using MormorDB.Entities;
using MormorDB.DTOs;

namespace MormorDB.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController(MormorDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> ListSupllier()
    {
        var suppliers = await context.Suppliers
        .Include(p => p.SupplierProducts)
        .ThenInclude(p => p.Product)
        .ToListAsync();

        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSupllierById(int id)
    {
        var supplier = await context.Suppliers
        .Include(s => s.SupplierProducts)
        .ThenInclude(sp => sp.Product)
        .FirstOrDefaultAsync(s => s.SupplierId == id);

        if (supplier is null) return NotFound();

        return Ok(supplier);
    }

    [HttpPost("{supplierId}/products")]
    public async Task<ActionResult> AddProductToSupplier(int supplierId, PostProductDto dto)
    {
        var supplier = await context.Suppliers.FindAsync(supplierId);
        if (supplier is null) return NotFound("Leverantören hittades inte");

        var product = new Product
        {
            ArticleNumber = dto.ArticleNumber,
            ProductName = dto.ProductName
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var supplierProduct = new SupplierProduct
        {
            SupplierId = supplierId,
            ProductId = product.ProductId,
            PriceKg = dto.PriceKg
        };

        context.SupplierProducts.Add(supplierProduct);
        await context.SaveChangesAsync();

        return StatusCode(201, "Produkt tillagd");
    }

    [HttpPatch("{supplierId}/products/{ProductId}")]
    public async Task<ActionResult> UpdatePrice(int supplierId, int ProductId, PatchPriceDto dto)
    {
        var supplierProduct = await context.SupplierProducts
        .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == ProductId);

        if (supplierProduct is null) return NotFound("Hittade inte produkten hos den kunden");

        supplierProduct.PriceKg = dto.PriceKg;
        await context.SaveChangesAsync();

        return NoContent();

    }
}
