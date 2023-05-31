using ShopChainManagement.Models;
using ShopChainManagement.Repository;

namespace ShopChainManagement
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Shop> ShopRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        Task<bool> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopChainDbContext _dbContext;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Shop> _shopRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Transaction> _transactionRepository;

        public UnitOfWork(ShopChainDbContext dbContext)
        {
            _dbContext = dbContext;
            _customerRepository = new Repository<Customer>(dbContext);
            _shopRepository = new Repository<Shop>(dbContext);
            _productRepository = new Repository<Product>(dbContext);
            _orderRepository = new Repository<Order>(dbContext);
            _transactionRepository = new Repository<Transaction>(dbContext);
        }

        public IRepository<Customer> CustomerRepository => _customerRepository;

        public IRepository<Shop> ShopRepository => _shopRepository;

        public IRepository<Product> ProductRepository => _productRepository;

        public IRepository<Order> OrderRepository => _orderRepository;

        public IRepository<Transaction> TransactionRepository => _transactionRepository;

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}