using AutoMapper;
using BandAPI.Dtos;
using BandAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandCollectionsController : ControllerBase
    {

        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;

        public BandCollectionsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumRepository = bandAlbumRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult<List<BandDto>> CreateBandCollection([FromBody] IEnumerable<BandForCreatingDto> bandColl)
        {
            var bandEntities = _mapper.Map<IEnumerable<Entities.Band>>(bandColl);
            foreach (var band in bandEntities)
            {
                _bandAlbumRepository.AddBand(band);
            }
            _bandAlbumRepository.Save();

            return Ok();
        }
    }
}
