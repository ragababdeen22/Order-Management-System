namespace OrderManagementSystem.Models
{
    public class OrderService
    {
        private readonly OrderManagementDbContext _context;

        public OrderService(OrderManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            decimal total = 0;

            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId)
                    ?? throw new Exception("Product not found");

                if (product.Stock < item.Quantity)
                    throw new Exception("Insufficient stock");

                product.Stock -= item.Quantity;

                item.UnitPrice = product.Price;
                total += item.Quantity * item.UnitPrice;
            }

            order.TotalAmount = DiscountService.ApplyDiscount(total);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _context.Invoices.Add(new Invoice
            {
                OrderId = order.OrderId,
                TotalAmount = order.TotalAmount
            });

            await _context.SaveChangesAsync();
            return order;
        }
    }

}
