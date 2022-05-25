using AutoMapper;
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
            CreateMap<Character, GetCharacterDto>();
            CreateMap<Character, AddCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Weapon, AddWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HightscoreDto>();



            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
            //CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            //CreateMap<Weapon, AddWeaponDto>().ReverseMap();

        }
    }
}
