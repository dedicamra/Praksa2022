using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.CharacterSkills
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);  
    }
}
