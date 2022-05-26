using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication3._1.Dtos.Fights;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.FightService
{
    public interface IFigthService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request);
        Task<ServiceResponse<List<HightscoreDto>>> GetHighscore();

    }
}
