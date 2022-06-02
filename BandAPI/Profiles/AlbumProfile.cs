﻿using AutoMapper;

namespace BandAPI.Profiles
{
    public class AlbumProfile :Profile
    {
       public AlbumProfile()
        {
            CreateMap<Entities.Album, Dtos.AlbumDto>().ReverseMap();
        }
    }
}