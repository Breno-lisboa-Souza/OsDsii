using OsDsii.api.Models;

namespace OsDsii.api.Services.Interaces
{
    public interface ICustomersService
    {
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public Task<Customer> GetCustomerByIdAsync(int id);
    }
}