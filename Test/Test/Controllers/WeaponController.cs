using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test.Dtos.Character;
using Test.Dtos.Weapon;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        public IWeaponService _weaponService;

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
