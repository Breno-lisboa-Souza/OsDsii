using Microsoft.EntityFrameworkCore;
using OsDsii.api.Data;
using OsDsii.api.Models;
using OsDsii.api.Repositories.Interfaces;

namespace OsDsii.api.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DataContext _context;

        public CustomersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() 
        {
            IEnumerable<Customer> customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => id == c.Id);
        }
    }
}