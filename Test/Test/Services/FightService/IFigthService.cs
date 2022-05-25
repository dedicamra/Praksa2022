using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Dtos.Fights;
using Test.Models;

namespace Test.Services.FightService
{
    public interface IFigthService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request);
        Task<ServiceResponse<List<HightscoreDto>>> GetHighscore();

    }
}
