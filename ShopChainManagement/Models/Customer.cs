using System.ComponentModel.DataAnnotations.Schema;

namespace ShopChainManagement.Models
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        // Danh sách đơn hàng của khách hàng
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
