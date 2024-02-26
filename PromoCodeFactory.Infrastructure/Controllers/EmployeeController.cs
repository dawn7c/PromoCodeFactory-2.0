using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.Administration;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using PromoCodeFactory.Infrastructure.Models;

namespace PromoCodeFactory.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _roleRepository;

        public EmployeeController(ApplicationContext context, IRepository<Employee> employeeRepository, IRepository<Role> roleRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetAllAsync()
        {
            var employee = await _employeeRepository.GetAllAsync();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeAddAsync(EmployeeCreateRequest employeeRequest)
        {
            var employeeId = Guid.NewGuid();
            
            var roleId = _context.Roles.Where(e => e.Description == "Сотрудник").FirstOrDefault().Id;

            // Создаем сотрудника
            var employee = new Employee()
            {
                Id = employeeId,
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                Email = employeeRequest.Email,
                RoleId = roleId 
            };

            // Добавляем сотрудника в базу данных
            await _employeeRepository.AddAsync(employee);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var customer = await _employeeRepository.GetByIdAsync(id);
            if (customer is null)
                return NotFound("Сотрудник не найден.");

            await _employeeRepository.RemoveAsync(customer);
            return Ok();
        }

    }
}
