using OsDsii.api.Models;

namespace OsDsii.api.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer> GetCustomerByIdAsync(int id);
    }
}
