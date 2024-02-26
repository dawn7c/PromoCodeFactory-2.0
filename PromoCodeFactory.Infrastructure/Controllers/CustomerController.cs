using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using PromoCodeFactory.Infrastructure.Models;

namespace PromoCodeFactory.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Customer> _customerRepository;


        public CustomerController(IRepository<Customer> customerRepository, ApplicationContext context)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerResponse>> GetAllAsync()
        {
            var customer = await _customerRepository.GetAllAsync();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerAddAsync(CustomerCreateRequest customerRequest)
        {
            var customerId = Guid.NewGuid();

            var customer = new Customer()
            {
                Id = customerId,
                FirstName = customerRequest.FirstName,
                LastName = customerRequest.LastName,
                Email = customerRequest.Email
                //Preferences = _context.CustomerPreferences.Where(e=> e.PreferenceId == customerRequest.PreferenceId).ToList()
                /*Preferences = customerRequest.PreferenceIds.Select(p => new CustomerPreference { PreferenceName = p }).ToList()*/,
            };
            await _customerRepository.AddAsync(customer);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is null)
                return NotFound("Клиент не найден.");

            await _customerRepository.RemoveAsync(customer);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerCreateRequest request)
        {
         
                var customer = await _customerRepository.GetByIdAsync(id);

                if (customer is null)
                    return NotFound("Клиент не найден.");

                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Email = request.Email;
               
                await _customerRepository.UpdateAsync(customer);

                return Ok();
            
        }
    }
}
