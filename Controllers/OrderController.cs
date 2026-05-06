namespace MormorDB.Controllers;

using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDB.Data;
using MormorDB.Entities;

[ApiController]
[Route("api/[controller]")]
public class OrderController(MormorDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(Order order)
    {
        foreach (var orderItem in order.OrderLines)
        {
            var product = await context.Products.FindAsync(orderItem.ProductId);
            if (product is null) return BadRequest($"Product with id {orderItem.ProductId} not found.");

            orderItem.Price = product.Price;
        }

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Create), new { id = order.OrderId }, order);
    }

    [HttpGet("number/{orderNumber}")]
    public async Task<ActionResult<Order>> GetOrderByName(string orderNumber)
    {
    var order = await context.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderLines)
        .ThenInclude(ol => ol.Product)
        .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpGet("date/{orderDate}")]
    public async Task<ActionResult<Order>> GetOrderByDate(DateTime orderDate)
    {
        var order = await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Product)
            .Where(o => o.OrderDate.Date == orderDate.Date)
            .ToListAsync();
        if (order is null) return NotFound();
        return Ok(order);
    }
}
