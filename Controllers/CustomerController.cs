using Microsoft.AspNetCore.Mvc;
using MormorDB.Data;
using Microsoft.EntityFrameworkCore;
using MormorDB.Entities;

namespace MormorDB.Controllers;

    [ApiController]
    [Route("api/customers")]
    public class CustomerController(MormorDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customers = await context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var customer = await context.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.OrderLines)
            .ThenInclude(ol => ol.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer is null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPatch("{id}/contactperson")]
        public async Task<ActionResult> UpdateCustomerContactPerson(int id, string contactPerson)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer is null) return NotFound();

            customer.ContactPerson = contactPerson;
            await context.SaveChangesAsync();
            return Ok(customer);
        }

    }
