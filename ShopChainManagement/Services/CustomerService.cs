using ShopChainManagement.Models;

namespace ShopChainManagement.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return customers.OrderBy(x => x.Email).ToList();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _unitOfWork.CustomerRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.SearchAsync(c => c.Email.Equals(customer.Email));

            if (existingCustomer != null && existingCustomer.Any())
            {
                return false;
            }

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(customer.Id);

            if (existingCustomer != null)
            {
                return false;
            }

            _unitOfWork.CustomerRepository.Update(customer);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);

            if (existingCustomer != null)
            {
                return false;
            }

            await _unitOfWork.CustomerRepository.RemoveAsync(id);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
