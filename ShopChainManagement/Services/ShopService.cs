using ShopChainManagement.Models;

namespace ShopChainManagement.Services
{
    public interface IShopService
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync();
        Task<bool> CreateShopAsync(Shop shop);
        Task<Shop> GetShopByIdAsync(int id);
        Task<bool> UpdateShopAsync(Shop shop);
        Task<bool> DeleteShopAsync(int id);
    }

    public class ShopService : IShopService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateShopAsync(Shop shop)
        {
            await _unitOfWork.ShopRepository.AddAsync(shop);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            var shops = await _unitOfWork.ShopRepository.GetAllAsync();
            return shops.OrderByDescending(shop => shop.Location).ToList();
        }

        public Task<Shop> GetShopByIdAsync(int id)
        {
            return _unitOfWork.ShopRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateShopAsync(Shop shop)
        {
            _unitOfWork.ShopRepository.Update(shop);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            await _unitOfWork.ShopRepository.RemoveAsync(id);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
