﻿using AutoMapper;
using BandAPI.Dtos;
using BandAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandAPI.Controllers
{
    [Route("api/bands/{bandId}/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbumsForBand(Guid bandId)
        {
            if (!_bandAlbumRepository.BandExists(bandId)) 
                return NotFound();

            var albumsFromRepo = _bandAlbumRepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albumsFromRepo));
        }
        [HttpGet("{albumId}", Name = "GetAlbumForBand")]
        public ActionResult<AlbumDto>GetAlbumForBand(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var albumFromRepo = _bandAlbumRepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumDto>(albumFromRepo));
        }
        [HttpPost]
        public ActionResult<AlbumDto> CreateAlbumForBand(Guid bandId, [FromBody]AlbumForCreatingDto create)
        {
            if (!_bandAlbumRepository.BandExists(bandId))
                return NotFound();
            var album=_mapper.Map<Entities.Album>(create);
            _bandAlbumRepository.AddAlbum(bandId, album);
            _bandAlbumRepository.Save();

            var albumToReturn=_mapper.Map<AlbumDto>(album);
            return CreatedAtRoute("GetAlbumForBand", new {bandId=bandId, albumId=albumToReturn.Id},albumToReturn);
        }
    }
}
