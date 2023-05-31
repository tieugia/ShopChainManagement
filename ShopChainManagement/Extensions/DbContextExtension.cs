using Microsoft.EntityFrameworkCore;
using ShopChainManagement;
using ShopChainManagement.Models;

public static class DbInitializer
{
    public static async Task Initialize(ShopChainDbContext context)
    {
        if (!context.Customers.Any())
        {
            var customers = Enumerable.Range(1, 30).Select(i => new Customer
            {
                Name = $"Customer {i}",
                DateOfBirth = DateTime.UtcNow.AddYears(-25).AddDays(i),
                Email = $"customer{i}@example.com"
            });
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }

        if (!context.Shops.Any())
        {
            var shops = new List<Shop>
            {
                new Shop { Name = "Shop A", Location = "Location A" },
                new Shop { Name = "Shop B", Location = "Location B" },
                new Shop { Name = "Shop C", Location = "Location C" }
            };
            await context.Shops.AddRangeAsync(shops);
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            var products = Enumerable.Range(1, 30).Select(i => new Product
            {
                Name = $"Product {i}",
                Price = 1000 * i
            });
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        if (!context.Orders.Any())
        {
            var orders = new List<Order>();
            var shops = await context.Shops.ToListAsync();
            var customers = await context.Customers.ToListAsync();
            var random = new Random();
            for (int i = 1; i <= 50; i++)
            {
                var shop = shops[random.Next(shops.Count)];
                var customer = customers[random.Next(customers.Count)];
                orders.Add(new Order
                {
                    ShopId = shop.Id,
                    CustomerId = customer.Id
                });
            }
            await context.Orders.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }

        if (!context.Transactions.Any())
        {
            var transactions = new List<Transaction>();
            var orders = await context.Orders.ToListAsync();
            var products = await context.Products.ToListAsync();
            var random = new Random();
            for (int i = 1; i <= 60; i++)
            {
                var order = orders[random.Next(orders.Count)];
                var product = products[random.Next(products.Count)];
                var quantity = random.Next(1, 5);
                var amount = product.Price * quantity;
                transactions.Add(new Transaction
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Price = product.Price,
                    Quantity = quantity,
                    Amount = amount
                });
            }
            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }
    }
}
