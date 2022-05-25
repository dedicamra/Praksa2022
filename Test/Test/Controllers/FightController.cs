using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test.Dtos.Fights;
using Test.Models;
using Test.Services.FightService;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly IFigthService _fightService;

        public FightController(IFigthService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto request)
        {
            var result = await _fightService.WeaponAttack(request);

            return Ok(result);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttack(SkillAttackDto request)
        {
            var result = await _fightService.SkillAttack(request);

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(FightRequestDto request)
        {
            var result = await _fightService.Fight(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<HightscoreDto>>> GetHighscore()
        {
            return Ok(await _fightService.GetHighscore());  
        }
    }
}
