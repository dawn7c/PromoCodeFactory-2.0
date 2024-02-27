using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using PromoCodeFactory.Infrastructure.Models;
using System.Runtime.CompilerServices;

namespace PromoCodeFactory.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromoCodeController(ApplicationContext context, IRepository<PromoCode> promoCodeRepository, IRepository<Preference> preferencesRepository, IRepository<Customer> customersRepository)
        {
            _context = context;
            _promoCodeRepository = promoCodeRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PromoCodeResponse>> PromoCodesGetAllAsync()
        {
            var preferences = await _promoCodeRepository.GetAllAsync();

            var response = preferences.Select(x => new PromoCodeResponse()
            {
                Id = x.Id,
                Code = x.Code,
                BeginDate = x.BeginDate,
                EndDate = x.EndDate,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PromoCodeAddAsync(PromoCodeResponse promoResponse, string namePref, string partnerName)
        {
            var promoCodePreference = _context.Preferences.FirstOrDefault(e => e.Name == namePref);
            var partner = _context.Partners.FirstOrDefault(e => e.PartnerName == partnerName);
            var promoCodeId = Guid.NewGuid();

            var promoCode = new PromoCode()
            {
                Id = promoCodeId,
                Code = promoResponse.Code,
                ServiceInfo = promoResponse.ServiceInfo,
                BeginDate = promoResponse.BeginDate,
                EndDate = promoResponse.EndDate,
                
            };

            await _promoCodeRepository.AddAsync(promoCode);
            return Ok();
        }
    }
}
