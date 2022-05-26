using AutoMapper;
using System.Linq;
using WebApplication3._1.Dto.CharacterSkill;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Dtos.Fights;
using WebApplication3._1.Dtos.Skills;
using WebApplication3._1.Dtos.Weapon;
using WebApplication3._1.Models;

namespace WebApplication3._1
{
    public class AuotoMapperProfile : Profile
    {
        public AuotoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>()
                //.ForMember( dto=>dto.Skills, opt=>opt.MapFrom(c=>c.CharacerSkills.Select(cs=>cs.Skill)));
                .ForMember( dto=>dto.Skills, 
                            opt=>opt.MapFrom(src=> src.CharacterSkills.Select(x=>x.Skill)));
            CreateMap<Character, AddCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<CharacterSkill, CharacterSkillDto>();

            CreateMap<Weapon, AddWeaponDto>();
            CreateMap<Character, HightscoreDto>();



            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<Weapon, AddWeaponDto>().ReverseMap();

        }
    }
}
