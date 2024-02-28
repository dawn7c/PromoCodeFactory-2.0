using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using PromoCodeFactory.Infrastructure.Models;

namespace PromoCodeFactory.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Preference> _preferenceRepository;

        public PreferenceController (ApplicationContext context, IRepository<Preference> preferenceRepository)
        {
            _context = context;
            _preferenceRepository = preferenceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PreferenceResponse>> PreferenceGetAllAsync()
        {
           var pref = await _preferenceRepository.GetAllAsync();
            return Ok(pref);
        }

        [HttpPost]
        public async Task<IActionResult> PreferenceAddAsync(PreferenceRequest prefRequest)
        {
            if (_context.Preferences.Any(p => p.Name == prefRequest.Name))
                return BadRequest("Данное предпочтение уже существует в БД");
            
            var prefId = Guid.NewGuid();
            var preference = new Preference()
            {
                Id = prefId,
                Name = prefRequest.Name,
            };
            await _preferenceRepository.AddAsync(preference);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> PreferenceDeleteAsync(Guid id)
        {
            var preference = await _preferenceRepository.GetByIdAsync(id);
            if (preference is null)
                return NotFound("Предпочтение не найдено.");
            await _preferenceRepository.RemoveAsync(preference);
            return Ok();
        }
    }
}
