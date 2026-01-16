using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class CustomerController : ControllerBase
    //{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public CustomerController(OrderManagementDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [Authorize]
        [HttpGet("{customerId}/orders")]
        public IActionResult GetCustomerOrders(int customerId)
        {
            var orders = _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToList();

            return Ok(orders);
        }
    }


    //}
}
