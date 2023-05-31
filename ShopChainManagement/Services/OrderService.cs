using ShopChainManagement.Models;

namespace ShopChainManagement.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(Order Order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(Order Order);
        Task<bool> DeleteOrderAsync(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int OrderId)
        {
            return await _unitOfWork.OrderRepository.GetByIdAsync(OrderId);
        }

        public async Task<bool> UpdateOrderAsync(Order Order)
        {
            _unitOfWork.OrderRepository.Update(Order);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            await _unitOfWork.OrderRepository.RemoveAsync(id);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CreateOrderAsync(Order Order)
        {
            await _unitOfWork.OrderRepository.AddAsync(Order);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
