using OsDsii.api.Models;
using OsDsii.api.Repositories.Interfaces;
using OsDsii.api.Services.Interaces;
using OsDsii.api.Repositories.UnitOfWork;

namespace OsDsii.api.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersService(ICustomersRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customersRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            Customer customer = await _customersRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("Not found");
            }

            return customer;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            Customer currentCustomer = await _customersRepository.GetCustomerByIdAsync(customer.Id);
            if (currentCustomer != null && currentCustomer.Equals(customer))
            {
                throw new Exception("Customer already exists.");
            }
            await _customersRepository.CreateCustomerAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return currentCustomer;
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            Customer currentCustomer = await _customersRepository.GetCustomerByIdAsync(id);
            if (currentCustomer is null)
            {
                throw new Exception("Not found");
            }

            currentCustomer.Name = customer.Name;
            currentCustomer.Email = customer.Email;
            currentCustomer.Phone = customer.Phone;
            await _unitOfWork.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteCustomerAsync(int id, Customer customer)
        {
            Customer currentCustomer = await _customersRepository.GetCustomerByIdAsync(id);
            if(currentCustomer is null)
            {
                throw new Exception("Customer not found");
            }

            _customersRepository.RemoveCustomer(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}