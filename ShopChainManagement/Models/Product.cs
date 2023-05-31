using System.ComponentModel.DataAnnotations.Schema;

namespace ShopChainManagement.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Shop> Shops { get; set; } = new List<Shop>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
