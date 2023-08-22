﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OsDsii.api.Data;
using OsDsii.api.Models;
using OsDsii.api.Services.Interaces;

namespace OsDsii.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ICustomersService _customersService;
        public CustomersController(DataContext dataContext, ICustomersService customersService)
        {
            _dataContext = dataContext;
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<Customer> customers = await _customersService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            try
            {
                Customer customer = await _customersService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                Customer currentCustomer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
                // REGRA QUE VERIFICA SE USUÁRIO JA EXISTE
                if (currentCustomer != null && currentCustomer.Equals(customer))
                {
                    return BadRequest("Usuário já existe");
                }
                await _dataContext.AddAsync(customer);
                await _dataContext.SaveChangesAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, [FromBody] Customer customer)
        {
            try
            {
                Customer currentCustomer = await _dataContext.Customers.FirstOrDefaultAsync(c => id == c.Id);
                if (currentCustomer is null)
                {
                    throw new Exception("Not found");
                }
                currentCustomer.Name = customer.Name;
                currentCustomer.Email = customer.Email;
                currentCustomer.Phone = customer.Phone;
                await _dataContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                Customer customer = await _dataContext.Customers.FirstOrDefaultAsync(c => id == c.Id);
                if (customer is null)
                {
                    throw new Exception("Not found");
                }
                _dataContext.Customers.Remove(customer);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}