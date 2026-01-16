using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Models
{
    public class OrderManagementDbContext:DbContext
    {
        public OrderManagementDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
