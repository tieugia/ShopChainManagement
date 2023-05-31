using ShopChainManagement.Models;

namespace ShopChainManagement.Services
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> SearchProductAsync(string? searchTerm);
    }

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string? searchTerm)
        {
            var products = string.IsNullOrWhiteSpace(searchTerm) ? await _unitOfWork.ProductRepository.GetAllAsync() : await _unitOfWork.ProductRepository.SearchAsync(product => product.Name.Contains(searchTerm));
            return products.OrderBy(p => p.Name).ToList();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            await _unitOfWork.ProductRepository.RemoveAsync(id);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
