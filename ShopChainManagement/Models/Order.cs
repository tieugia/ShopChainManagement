using System.ComponentModel.DataAnnotations.Schema;

namespace ShopChainManagement.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int CustomerId { get; set; }

        public Shop Shop { get; set; }
        public Customer Customer { get; set; }
        public Transaction Transaction { get; set; }
    }
}
