using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using PromoCodeFactory.Infrastructure.Models;


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
        public async Task<IActionResult> PromoCodesGetAllAsync()
        {
            var preferences = await _promoCodeRepository.GetAllAsync();
            return Ok(preferences);
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

            var promoCode = new PromoCode(promoResponse.Code, promoResponse.ServiceInfo, promoResponse.BeginDate, promoResponse.EndDate, partner.Id, promoCodePreference.Id, customer.Id);
            
            await _promoCodeRepository.AddAsync(promoCode);
            return Ok();
        }
    }
}
