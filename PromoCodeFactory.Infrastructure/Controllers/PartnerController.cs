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
    public class PartnerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Partner> _partnerRepository;

        [HttpGet]
        public async Task<ActionResult<PartnerResponse>> PartnerGetAllAsync()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return Ok(partners);
        }

        // Сделать маппинг, должен записывать данные с Employee, а не вводить руками, дописать Request and Response
        [HttpPost]
        public async Task<IActionResult> PartnerAddAsync(PartnerRequest request)
        {
            var partnerId = Guid.NewGuid();
            var partner = new Partner()
            {
                PartnerName = request.PartnerName,
                PartnerManager = request.PartnerManager,
                PartnerManagerId = partnerId,

            };
            await _partnerRepository.AddAsync(partner);
            return Ok();
        }
    }
}
