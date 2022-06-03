using AutoMapper;

namespace BandAPI.Profiles
{
    public class AlbumProfile :Profile
    {
       public AlbumProfile()
        {
            CreateMap<Entities.Album, Dtos.AlbumDto>().ReverseMap();
            CreateMap<Dtos.AlbumForCreatingDto, Entities.Album>();
            CreateMap<Dtos.AlbumForUpdateDto, Entities.Album>();
            CreateMap<Dtos.AlbumForUpdateDto, Entities.Album>().ReverseMap();
        }
    }
}
