
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.Administration;
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
        public async Task<IActionResult> EmployeeAddAsync([FromBody]EmployeeCreateRequest employeeRequest)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e =>                                    
                                                                            e.FirstName == employeeRequest.FirstName &&
                                                                            e.LastName == employeeRequest.LastName &&
                                                                            e.Email == employeeRequest.Email);

            if (existingEmployee != null)
                return BadRequest("Сотрудник с такими данными уже существует");
            
            var role = _context.Roles.Where(e => e.Description == employeeRequest.roleDescription).FirstOrDefault();

            if (role == null)
                return BadRequest("Роль не найдена");
            
            // Создаем сотрудника
            var employee = new Employee(employeeRequest.FirstName, employeeRequest.LastName, employeeRequest.Email, role.Id);

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
