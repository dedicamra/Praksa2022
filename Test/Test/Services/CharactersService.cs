using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Data;
using Test.Dtos.Character;
using Test.Models;

namespace Test.Services
{
    public class CharactersService : ICharactersService
    {


        private readonly IMapper _mapper;
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharactersService(IMapper mapper, DataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newC);
            character.User = await _db.User.FirstOrDefaultAsync(x => x.Id == GetUserId());

            _db.Characters.Add(character);
            await _db.SaveChangesAsync();
            serviceResponse.Data = await _db.Characters.Where(x => x.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

                if (character != null)
                {

                    _db.Characters.Remove(character);

                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _db.Characters.Where(x => x.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                };

                //return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _db.Characters
                  .Include(x => x.Weapon)
                .Include(x => x.Skills).Where(x => x.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _db.Characters
                .Include(x=>x.Weapon)
                .Include(x=>x.Skills).FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch)
        {
            var serviceResponce = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _db.Characters.Include(x => x.User).FirstOrDefaultAsync(c => c.Id == ch.Id);

                if (character.User.Id == GetUserId())
                {

                    character.Name = ch.Name;
                    character.Strength = ch.Strength;
                    character.HitPoints = ch.HitPoints;
                    character.Defence = ch.Defence;
                    character.Intelligence = ch.Intelligence;
                    character.Class = ch.Class;

                    await _db.SaveChangesAsync();
                }
                else
                {
                    serviceResponce.Success = false;
                    serviceResponce.Message = "Character not found.";
                }

                serviceResponce.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }


            return serviceResponce;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _db.Characters.Include(x => x.Weapon).Include(x => x.Skills).FirstOrDefaultAsync(x => x.Id == newSkill.CharacterId && x.User.Id == GetUserId());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var skill= await _db.Skills.FirstOrDefaultAsync(x => x.Id == newSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                character.Skills.Add(skill);
                await _db.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


    }
}
