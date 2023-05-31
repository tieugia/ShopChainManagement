using System.ComponentModel.DataAnnotations.Schema;

namespace ShopChainManagement.Models
{
    [Table("Shop")]
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        // Danh sách sản phẩm của cửa hàng
        public ICollection<Product> Products { get; set; } = new List<Product>();
        // Danh sách đơn hàng của cửa hàng
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
