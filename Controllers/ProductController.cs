using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public ProductController(OrderManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts() => Ok(_context.Products);

        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            return product == null ? NotFound() : Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, Product updated)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;

            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }

}
