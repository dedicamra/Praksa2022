﻿using AutoMapper;
using BandAPI.Dtos;
using BandAPI.Helpers;
using BandAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _bandAlbumRepository = bandAlbumRepository ??
               throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetBandCollection")]
        public IActionResult GetBandCollection([FromRoute]
               [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var bandEntities = _bandAlbumRepository.GetBands(ids);

            if (ids.Count() != bandEntities.Count())
                return NotFound();

            var bandsToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);

            return Ok(bandsToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody] IEnumerable<BandForCreatingDto> bandColl)
        {
            var bandEntities = _mapper.Map<IEnumerable<Entities.Band>>(bandColl);
            foreach (var band in bandEntities)
            {
                _bandAlbumRepository.AddBand(band);
            }
            _bandAlbumRepository.Save();


            _bandAlbumRepository.Save();
            var bandCollectionToReturn = _mapper.Map<IEnumerable<BandDto>>(bandEntities);
            var IdsString = string.Join(",", bandCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetBandCollection", new { ids = IdsString }, bandCollectionToReturn);
        }
    }
}
