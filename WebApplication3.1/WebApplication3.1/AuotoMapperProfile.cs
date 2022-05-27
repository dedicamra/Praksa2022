using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dto => dto.Skills,
                            opt=>opt.MapFrom(src=> src.CharacterSkills.Select(x=>x.Skill)));
                            //opt => opt.MapFrom(src => src.CharacterSkills.Select(x => x.Skill.Name+" "+x.Skill.Damage).ToList()));
            CreateMap<Character, AddCharacterDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            //CreateMap<List<GetSkillDto>, GetCharacterDto>();

            CreateMap<Weapon, AddWeaponDto>();
            CreateMap<Character, HightscoreDto>();



            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponDto>().ReverseMap();
            CreateMap<Weapon, AddWeaponDto>().ReverseMap();
            CreateMap<Skill, GetSkillDto>().ReverseMap();
            //CreateMap<CharacterSkill, CharacterSkillDto>().ReverseMap();
           // CreateMap<List<GetSkillDto>, GetCharacterDto>().ReverseMap();




        }
    }
}
