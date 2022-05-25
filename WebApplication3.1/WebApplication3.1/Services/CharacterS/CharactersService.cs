using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3._1.Data;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.CharacterS
{
    public class CharactersService : ICharactersService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _db;

        public CharactersService(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newC);
            await _db.Characters.AddAsync(character);
            await _db.SaveChangesAsync();
            serviceResponse.Data = _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character != null)
                {

                    _db.Characters.Remove(character);

                    await _db.SaveChangesAsync();
                    serviceResponse.Data = _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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


        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _db.Characters.Where(x=>x.Id==userId)
                 // .Include(x => x.Weapon)
              //  .Include(x => x.Skills)
              .ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _db.Characters
                //.Include(x => x.Weapon)
                //.Include(x => x.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch)
        {
            var serviceResponce = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == ch.Id);

                //if (character.User.Id == GetUserId())
                //{

                    character.Name = ch.Name;
                    character.Strength = ch.Strength;
                    character.HitPoints = ch.HitPoints;
                    character.Defence = ch.Defence;
                    character.Intelligence = ch.Intelligence;
                    character.Class = ch.Class;
                    
                _db.Characters.Update(character);
                    await _db.SaveChangesAsync();
                //}
                //else
                //{
                //    serviceResponce.Success = false;
                //    serviceResponce.Message = "Character not found.";
                //}

                serviceResponce.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }


            return serviceResponce;
        }
    }
}
