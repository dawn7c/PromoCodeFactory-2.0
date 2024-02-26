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
            var roleId = Guid.NewGuid();
            var role = new Role()
            {
                Id = roleId,
                Name = roleRequest.Name,
                Description = roleRequest.Description,
            };
            await _roleRepository.AddAsync(role);
            return Ok();
        }
    }
}
