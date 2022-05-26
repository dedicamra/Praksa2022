using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3._1.Data;
using WebApplication3._1.Dtos.Fights;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services.FightService
{
    public class FigthService : IFigthService
    {

        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public FigthService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //128 video
        
        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var characters = await _db.Characters.
                    Include(x => x.Weapon).Include(x => x.CharacterSkills).ThenInclude(x => x.Skill)
                    .Where(x => request.CharacterIds.Contains(x.Id)).ToListAsync();

                bool defeated = false;

                while (!defeated)
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(x => x.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count())];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        //use weapon or skill to do attack
                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponDamage(attacker, opponent);

                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharacterSkills.Count);
                            attackUsed = attacker.CharacterSkills[randomSkill].Skill.Name;
                            damage = DoSkillAttack(attacker, opponent, attacker.CharacterSkills[randomSkill]);
                        }

                        response.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;

                            response.Data.Log.Add($"{opponent.Name} has been defeated!");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} hitpoint left!");
                            break;
                        }

                    }
                }

                //increase number of fights and reset hitpoints to 100
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                _db.Characters.UpdateRange(characters);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }


            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _db.Characters.Include(x=>x.CharacterSkills).ThenInclude(x => x.Skill).FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                var opponent = await _db.Characters.Include(x => x.CharacterSkills).ThenInclude(x => x.Skill).FirstOrDefaultAsync(x => x.Id == request.OpponentId);

                var characterSkill = attacker.CharacterSkills.FirstOrDefault(x => x.SkillId == request.SkillId);
                if (characterSkill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know this skill";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, characterSkill);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated";

                await _db.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoSkillAttack(Character attacker, Character opponent, CharacterSkill characterSkills)
        {
            //skill damage value + random number between 0 and attacker inteligence
            int damage = characterSkills.Skill.Damage + (new Random().Next(attacker.Intelligence));

            damage -= new Random().Next(opponent.Defeats);

            //if the damage if below 0, than we substract the damage from the hitpoints of the opponent
            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _db.Characters.Include(x => x.Weapon).FirstOrDefaultAsync(x => x.Id == request.AttackerId);

                var opponent = await _db.Characters.Include(x => x.Weapon).FirstOrDefaultAsync(x => x.Id == request.OpponentId);

             
                int damage = DoWeaponDamage(attacker, opponent);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated";

                _db.Characters.Update(opponent);
                await _db.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoWeaponDamage(Character attacker, Character opponent)
        {
            //attacker damage value + random number between 0 and attacker strength
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));

            damage -= new Random().Next(opponent.Defeats);

            //if the damage if below 0, than we substract the damage from the hitpoints of the opponent
            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HightscoreDto>>> GetHighscore()
        {
            var characters = await _db.Characters.Where(x => x.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats).ToListAsync();

            var response = new ServiceResponse<List<HightscoreDto>>
            {
                Data = characters.Select(x => _mapper.Map<HightscoreDto>(x)).ToList()
            };

            return response;
        }
    }
}
