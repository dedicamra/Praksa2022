using AutoMapper;
using BandAPI.Dtos;
using BandAPI.Helpers;
using BandAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandResourceParameters param)
        {
            var bandsFromRepo = _bandAlbumRepository.GetBands(param);
            

            return new JsonResult(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }
        [HttpGet("{bandId}")]
        public IActionResult GetBand(Guid bandId)
        {
            var bandsFromRepo = _bandAlbumRepository.GetBand(bandId);
            if (bandsFromRepo == null)
                return NotFound();

            return Ok(bandsFromRepo);
        }

    }
}
