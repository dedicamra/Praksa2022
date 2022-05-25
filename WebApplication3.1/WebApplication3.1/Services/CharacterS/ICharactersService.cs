using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.CharacterS
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
