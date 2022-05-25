using AutoMapper;
using Test.Dtos.Character;
using Test.Dtos.Fights;
using Test.Dtos.Skills;
using Test.Dtos.Weapon;
using Test.Models;

namespace Test
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
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<Weapon, AddWeaponDto>().ReverseMap();

        }
    }
}
