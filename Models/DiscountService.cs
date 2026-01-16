namespace OrderManagementSystem.Models
{
    public static class DiscountService
    {
        public static decimal ApplyDiscount(decimal total)
        {
            if (total > 200) return total * 0.90m;
            if (total > 100) return total * 0.95m;
            return total;
        }
    }

}
