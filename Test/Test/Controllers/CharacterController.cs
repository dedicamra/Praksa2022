using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Dtos.Character;
using Test.Models;
using Test.Services;

namespace Test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharactersService _service;

        public CharacterController(ICharactersService service)
        {
            _service = service;
        }



        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            
            
            return Ok(await _service.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _service.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Add([FromForm] AddCharacterDto newC)
        {

            return Ok(await _service.AddCharacter(newC));

        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto ch)
        {
            var response = await _service.UpdateCharacter(ch);
            if (response.Success)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var response = await _service.DeleteCharacter(id);
            if (response.Data!=null)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newSkill)
        {
            return Ok(await _service.AddCharacterSkill(newSkill));
        }


    }
}
