using Microsoft.AspNetCore.Http;
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
        [HttpPost("{id}")]
        public async Task<IActionResult> PartnerAddAsync(PartnerRequest request)
        {
            var partnerManagerRole = _context.Roles.FirstOrDefault(e => e.Description == "Партнерский менеджер");
           // var partnerManagerRole = _context.Roles.Where(e => e.Description == "Партнерский менеджер").FirstOrDefault();
            if (partnerManagerRole == null)
            {
                
                return BadRequest("Такой партнер не найден");
            }
            var partnerManager = _context.Employees.FirstOrDefault(e => e.RoleId == partnerManagerRole.Id);

            if (partnerManager == null)
            {
                return BadRequest("Сотрудник с ролью Партнерского менеджера не найден");
            }
            
            var partner = new Partner()
            {
                Company = request.Company,
                PartnerName = $"{partnerManager.FirstName} {partnerManager.LastName}",
                PartnerManagerId = partnerManager.Id
            };
            await _partnerRepository.AddAsync(partner);
            return Ok();
        }
    }
}
