using OsDsii.api.Models;
using OsDsii.api.Repositories.Interfaces;
using OsDsii.api.Services.Interaces;

namespace OsDsii.api.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
         _customersRepository = customersRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customersRepository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            Customer customer = await _customersRepository.GetCustomerByIdAsync(id);

            if(customer == null)
            {
                throw new Exception("Not found");
            }

            return customer;
        }
    }
}