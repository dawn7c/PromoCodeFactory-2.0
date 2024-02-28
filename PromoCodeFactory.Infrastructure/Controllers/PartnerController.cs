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
    public class PartnerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Partner> _partnerRepository;
        private readonly IRepository<Employee> _employeeRepository;

        public PartnerController(ApplicationContext context, IRepository<Partner> partnerRepository, IRepository<Employee> employeeRepository)
        {
            _context = context;
            _partnerRepository = partnerRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PartnerResponse>> PartnerGetAllAsync()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return Ok(partners);
        }

        
        [HttpPost]
        public async Task<IActionResult> PartnerAddAsync([FromBody] PartnerRequest request)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == request.id);
            if (employee == null)
                return BadRequest("Сотрудник не найден");

            var partnerManagerRole = _context.Roles.FirstOrDefault(e => e.Description == "Партнерский менеджер");
            if (partnerManagerRole == null)
                return BadRequest("Такой партнер не найден");
            
            var partner = new Partner(request.Company, employee.FirstName +" "+ employee.LastName, employee.Id);
            await _partnerRepository.AddAsync(partner);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PartnerUpdateAsync([FromBody] PartnerResponse response)
        {
            var employee = await _partnerRepository.GetByIdAsync(response.id);
            if (employee is null)
                return NotFound("Партнер не найден");

            employee.Company = response.Company;
            employee.PartnerName = response.PartnerName;
            await _partnerRepository.UpdateAsync(employee);
            return Ok();
            
        }

        [HttpDelete]
        public async Task<IActionResult> PartnerDeleteAsync(Guid id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            if (partner is null)
                return NotFound("Партнер не найден");
             await _partnerRepository.RemoveAsync(partner);
            return Ok();
        }
    }
}
