using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;
using WebApplication3._1.Services.CharacterSkills;

namespace WebApplication3._1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService _charactersSkillService;

        public CharacterSkillController(ICharacterSkillService charactersSkillService)
        {
            _charactersSkillService = charactersSkillService;
        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            return Ok(await _charactersSkillService.AddCharacterSkill(newCharacterSkill));
        }
    }   
}
