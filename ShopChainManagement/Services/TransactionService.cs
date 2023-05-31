using ShopChainManagement.Models;

namespace ShopChainManagement.Services
{
    public interface ITransactionService
    {
        Task<bool> CreateTransactionAsync(Transaction transaction);
        Task<dynamic> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<bool> UpdateTransactionAsync(Transaction transaction);
        Task<bool> DeleteTransactionAsync(int id);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> GetAllTransactionsAsync()
        {
            var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            var shops = await _unitOfWork.ShopRepository.GetAllAsync();
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            var transactionsJoined = from tran in transactions
                                     join order in orders on tran.OrderId equals order.Id
                                     join customer in customers on order.CustomerId equals customer.Id
                                     join shop in shops on order.ShopId equals shop.Id
                                     join product in products on tran.ProductId equals product.Id
                                     select new
                                     {
                                         TransactionId = tran.Id,
                                         CustomerId = customer.Id,
                                         CustomerName = customer.Name,
                                         CustomerEmail = customer.Email,
                                         ProductId = product.Id,
                                         ProductName = tran.Product.Name,
                                         ShopId = shop.Id,
                                         ShopName = shop.Name,
                                         OrderId = order.Id
                                     };

            return transactionsJoined.ToList();                           
        }

        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            return await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId);
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            _unitOfWork.TransactionRepository.Update(transaction);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            await _unitOfWork.TransactionRepository.RemoveAsync(id);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CreateTransactionAsync(Transaction transaction)
        {
            await _unitOfWork.TransactionRepository.AddAsync(transaction);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
