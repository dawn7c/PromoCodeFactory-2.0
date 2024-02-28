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
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Email == customerRequest.Email);
            if (existingCustomer != null)
            {
                return BadRequest("Клиент с такими данными уже существует");
            }

            var customer = new Customer(customerRequest.FirstName, customerRequest.LastName, customerRequest.Email);
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

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody]CustomerCreateRequest request)
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
