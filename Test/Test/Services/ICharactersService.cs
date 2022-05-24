using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Dtos.Character;
using Test.Models;

namespace Test.Services
{
    public interface ICharactersService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newC);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto ch);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
   
    }
}
