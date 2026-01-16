using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{

    [ApiController]
    [Route("api/invoices")]
    [Authorize(Roles = "Admin")]
    public class InvoiceController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public InvoiceController(OrderManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetInvoices() => Ok(_context.Invoices);

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(int invoiceId)
        {
            var invoice = _context.Invoices.Find(invoiceId);
            return invoice == null ? NotFound() : Ok(invoice);
        }
    }

}
