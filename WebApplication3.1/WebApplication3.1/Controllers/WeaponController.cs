using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Dtos.Weapon;
using WebApplication3._1.Models;
using WebApplication3._1.Services;

namespace WebApplication3._1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto request)
        {
           //var response = new ServiceResponse();
            return Ok(await _weaponService.AddWeapon(request));
        }
    }
}
