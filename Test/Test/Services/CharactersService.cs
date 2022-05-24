using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CharactersService(IMapper mapper, DataContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            
            Character character = _mapper.Map<Character>(newC);
          
            _db.Characters.Add(character);
            await _db.SaveChangesAsync();
            serviceResponse.Data = await _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            //ja ne znam zasto mi obrise a vrati exception 
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _db.Characters.FirstAsync(c => c.Id == id);
                _db.Characters.Remove(character);

                await _db.SaveChangesAsync();
                serviceResponse.Data = _db.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                serviceResponse.Success = true;

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
            var dbCharacters = await _db.Characters
                .Where(x=>x.User.Id==userId).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _db.Characters.FirstOrDefaultAsync(c=>c.Id==id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch)
        {
            var serviceResponce = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _db.Characters.FirstOrDefaultAsync(c => c.Id == ch.Id);
                character.Name = ch.Name;
                character.Strength = ch.Strength;
                character.HitPoints = ch.HitPoints;
                character.Defence = ch.Defence;
                character.Intelligence = ch.Intelligence;
                character.Class = ch.Class;

                await _db.SaveChangesAsync();

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
