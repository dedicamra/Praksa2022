using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;
using WebApplication3._1.Services.CharacterS;

namespace WebApplication3._1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly Services.CharacterS.ICharactersService _service;

        public CharacterController(Services.CharacterS.ICharactersService service)
        {
            _service = service;
        }




        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            int id= int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _service.GetAllCharacters(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _service.GetCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Add( AddCharacterDto newC)
        {

            return Ok(await _service.AddCharacter(newC));

        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto ch)
        {
            var response = await _service.UpdateCharacter(ch);
            if (response.Success)
                return Ok(await _service.UpdateCharacter(ch));
            else
                return NotFound(response);
        }

        [HttpDelete("DeleteCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {
            var response= await _service.DeleteCharacter(id);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

    }
}
