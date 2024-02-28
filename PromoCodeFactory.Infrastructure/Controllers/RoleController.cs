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
    public class RoleController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Role> _roleRepository;

        public RoleController(ApplicationContext context, IRepository<Role> roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }


        [HttpGet]
        public async Task<ActionResult<RoleResponse>> GetRoleAsync()
        {
            var role = await _roleRepository.GetAllAsync();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAddAsync(RoleRequest roleRequest)
        {
            var role = new Role(roleRequest.Name, roleRequest.Description);
            await _roleRepository.AddAsync(role);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RoleDeleteAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                return NotFound("Роль не найдена");
            await _roleRepository.RemoveAsync(role);
            return Ok();
        }

        
    }
}
