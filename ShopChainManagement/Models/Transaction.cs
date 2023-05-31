using System.ComponentModel.DataAnnotations.Schema;

namespace ShopChainManagement.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => _amount = Price * Quantity;
        }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
