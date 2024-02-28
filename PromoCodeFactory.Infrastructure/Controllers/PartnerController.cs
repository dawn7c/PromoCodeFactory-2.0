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

        public PartnerController(ApplicationContext context, IRepository<Partner> partnerRepository)
        {
            _context = context;
            _partnerRepository = partnerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PartnerResponse>> PartnerGetAllAsync()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return Ok(partners);
        }

        // Сделать маппинг, должен записывать данные с Employee, а не вводить руками, дописать Request and Response
        [HttpPost]
        public async Task<IActionResult> PartnerAddAsync(Guid id, [FromBody] PartnerRequest request)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return BadRequest("Сотрудник не найден");
            var partnerManagerRole = _context.Roles.FirstOrDefault(e => e.Description == "Партнерский менеджер");
           
            if (partnerManagerRole == null)
            {
                
                return BadRequest("Такой партнер не найден");
            }
            
            var partner = new Partner()
            {
                Company = request.Company,
                PartnerName = $"{employee.FirstName} {employee.LastName}",
                PartnerManagerId = employee.Id
            };
            await _partnerRepository.AddAsync(partner);
            return Ok();
        }
    }
}
