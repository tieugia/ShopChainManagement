using Microsoft.EntityFrameworkCore;
using ShopChainManagement.Models;

namespace ShopChainManagement
{
    // Lớp DbContext đại diện cho toàn bộ cơ sở dữ liệu
    public class ShopChainDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ShopChainDbContext(DbContextOptions<ShopChainDbContext> options) : base(options) { }
    }
}
