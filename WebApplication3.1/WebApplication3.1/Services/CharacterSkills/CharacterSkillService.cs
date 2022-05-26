using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3._1.Data;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.CharacterSkills
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly DataContext _db;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CharacterSkillService(DataContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            
            var response=new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _db.Characters
                    .Include(x=>x.Weapon)
                    .Include(x=> x.CharacterSkills). ThenInclude(x=>x.Skill)
                    .FirstOrDefaultAsync(x => x.Id == newCharacterSkill.CharacterId && x.User.Id==GetUserId());
                if (character == null)
                {
                    response.Success= false;
                    response.Message = "Character not found!";
                    return response;

                }
                
                var skill= await _db.Skills.FirstOrDefaultAsync(x => x.Id == newCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found!";
                    return response;

                }

                var characterSkill = new CharacterSkill
                {
                    CharacterId = newCharacterSkill.CharacterId,
                    SkillId = newCharacterSkill.SkillId
                };

                await _db.CharacterSkills.AddAsync(characterSkill);
                await _db.SaveChangesAsync();
              
                
                //var ch = await _db.Characters
                //    .Include(x => x.Weapon)
                //    .Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                //    .FirstOrDefaultAsync(x => x.Id == newCharacterSkill.CharacterId && x.User.Id == GetUserId());
                
                //var test = _mapper.Map<GetCharacterDto>(ch);
                var res = _mapper.Map<GetCharacterDto>(character);
                response.Data = res;

            }
            catch ( Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
