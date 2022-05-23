using AutoMapper;
using Test.Dtos.Character;
using Test.Models;

namespace Test
{
    public class AuotoMapperProfile : Profile
    {
        public AuotoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<Character, AddCharacterDto>();



            CreateMap<Character, GetCharacterDto>().ReverseMap();
            CreateMap<Character, AddCharacterDto>().ReverseMap();


        }
    }
}
