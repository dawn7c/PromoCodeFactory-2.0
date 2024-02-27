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
        public async Task<IActionResult> PromoCodeAddAsync(PromoCodeResponse promoResponse)
        {
            var promoCodePreference = _context.Preferences.FirstOrDefault(e => e.Name == promoResponse.namePreference);
            if (promoCodePreference == null)
                return BadRequest("Предпочтение не найдено");
            var partner = _context.Partners.FirstOrDefault(e => e.PartnerName == promoResponse.PartnerName);
            if (partner == null)
                return BadRequest("Партнер не найден");
            var customer = _context.Customers.AsEnumerable().FirstOrDefault(e => e.FullName == promoResponse.FullName) ?? _context.Customers.FirstOrDefault();
            if (customer == null)
                return BadRequest("Клиент не найден");

            var promoCodeId = Guid.NewGuid();
            var promoCode = new PromoCode()
            {
                Id = promoCodeId,
                Code = promoResponse.Code,
                ServiceInfo = promoResponse.ServiceInfo,
                BeginDate = promoResponse.BeginDate,
                EndDate = promoResponse.EndDate,
                PartnerId = partner.Id,
                PreferenceId = promoCodePreference.Id,
                CustomerId = customer.Id
            };

            await _promoCodeRepository.AddAsync(promoCode);
            return Ok();
        }
    }
}
