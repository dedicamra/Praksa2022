using AutoMapper;
using BandAPI.Dtos;
using BandAPI.Entities;
using BandAPI.Helpers;
using BandAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumRepository;
        private readonly IMapper _mapper;
        private readonly IPropertyMappigService _propertyMappigService;
        private readonly IPropertyValidationService _propertyValidationService;
        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper, IPropertyMappigService propertyMappigService, IPropertyValidationService propertyValidationService)
        {
            _bandAlbumRepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappigService = propertyMappigService ??
                throw new ArgumentNullException(nameof(propertyMappigService));
            _propertyValidationService = propertyValidationService ??
                throw new ArgumentNullException(nameof(propertyValidationService));
        }


        //https://localhost:44349/api/bands?fields=id,name
        [HttpGet(Name = "GetBands")]
        [HttpHead]
        public IActionResult GetBands([FromQuery] BandResourceParameters param)
        {
            if (!_propertyMappigService.ValidMappingExistis<BandDto, Band>(param.OrderBy))
                return BadRequest();

            if (!_propertyValidationService.HasValidProperties<BandDto>(param.Fields))
                return BadRequest();

            var bandsFromRepo = _bandAlbumRepository.GetBands(param);

            var previousPageLink = bandsFromRepo.HasPrevious ?
                CreateBandsUri(param, UriType.PreviousPage) : null;

            var nextPageLink = bandsFromRepo.HasNext ?
                CreateBandsUri(param, UriType.NextPage) : null;

            var metaData = new
            {
                totalCount = bandsFromRepo.TotalCount,
                pageSize = bandsFromRepo.PageSize,
                currentPage = bandsFromRepo.CurrentPage,
                totalPages = bandsFromRepo.TotalPages,
                //previousPageLink = previousPageLink,
                //nextPageLink = nextPageLink
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData));

            var links = CreateLinksForBands(param,bandsFromRepo.HasNext,bandsFromRepo.HasPrevious);
            var shapeBands = _mapper.Map<IEnumerable<BandDto>>(bandsFromRepo).ShapeData(param.Fields);

            var shapeBandsWithLinks = shapeBands.Select(band =>
            {
                var bandAsDictionary = band as IDictionary<string, object>;
                var bandLinks = CreateLinksForBand((Guid)bandAsDictionary["Id"], null);
                bandAsDictionary.Add("links", bandLinks);
                return bandAsDictionary;
            });

            //return shaped bands with links as value for the value field and the links for the band resource
            //as the value for the links field
            var linkedCollectionResource = new
            {
                value = shapeBandsWithLinks,
                links
            };
            return Ok(linkedCollectionResource);

            //return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo).ShapeData(param.Fields));
        }

        [HttpGet("{bandId}", Name = "GetBand")]
        public IActionResult GetBand(Guid bandId, string fields)
        {
            if (!_propertyValidationService.HasValidProperties<BandDto>(fields))
                return BadRequest();

            var bandsFromRepo = _bandAlbumRepository.GetBand(bandId);

            if (bandsFromRepo == null)
                return NotFound();

            var links = CreateLinksForBand(bandId, fields);
            var linkResourceToReturn = _mapper.Map<BandDto>(bandsFromRepo).ShapeData(fields) as IDictionary<string, object>;

            linkResourceToReturn.Add("links", links);

            return Ok(linkResourceToReturn);
            //return Ok(_mapper.Map<BandDto>(bandsFromRepo).ShapeData(fields));
        }

        [HttpPost(Name ="CreateBand")]
        public ActionResult<BandDto> AddBand(BandForCreatingDto create)
        {
            var bandEntitty = _mapper.Map<Band>(create);
            _bandAlbumRepository.AddBand(bandEntitty);
            _bandAlbumRepository.Save();

            var bandToReturn = _mapper.Map<BandDto>(bandEntitty);
            var links = CreateLinksForBand(bandToReturn.Id, null);
            var linkResourceToReturn = bandToReturn.ShapeData(null) as IDictionary<string, object>;
            linkResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetBand", new { bandId = linkResourceToReturn["Id"] }, linkResourceToReturn);
        }

        [HttpOptions]
        public IActionResult GetBAndOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, DELETE, HEAD, OPTIONS");
            return Ok();
        }

        [HttpDelete("{bandId}", Name = "DeleteBand")]
        public ActionResult DeleteBand(Guid bandId)
        {
            var band = _bandAlbumRepository.GetBand(bandId);
            if (band == null)
                return NotFound();

            _bandAlbumRepository.DeleteBand(band);
            _bandAlbumRepository.Save();

            return NoContent();
        }


        private string CreateBandsUri(BandResourceParameters bandResourceParameters, UriType uriType)
        {
            switch (uriType)
            {
                case UriType.PreviousPage:
                    return Url.Link("GetBands", new
                    {
                        fields = bandResourceParameters.Fields,
                        orderBy = bandResourceParameters.OrderBy,
                        pageNumber = bandResourceParameters.PageNumber - 1,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery
                    });
                case UriType.NextPage:
                    return Url.Link("GetBands", new
                    {
                        fields = bandResourceParameters.Fields,
                        orderBy = bandResourceParameters.OrderBy,
                        pageNumber = bandResourceParameters.PageNumber + 1,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery
                    });
                case UriType.Current:
                default:
                    return Url.Link("GetBands", new
                    {
                        fields = bandResourceParameters.Fields,
                        orderBy = bandResourceParameters.OrderBy,
                        pageNumber = bandResourceParameters.PageNumber,
                        pageSize = bandResourceParameters.PageSize,
                        mainGenre = bandResourceParameters.MainGenre,
                        searchQuery = bandResourceParameters.SearchQuery
                    });
            }
        }

        private IEnumerable<LinkDto> CreateLinksForBand(Guid bandId, string fields)
        {
            var links = new List<LinkDto>();
            //self links; links that leads to a single band
            if (string.IsNullOrWhiteSpace(fields))
            {
                //fields are null, we need uri to the band resource
                links.Add(
                    new LinkDto(
                    Url.Link("GetBand", new { bandId = bandId }),
                    "self",
                    "GET"));

            }
            else
            {
                //fields are present
                links.Add(
                    new LinkDto(
                    Url.Link("GetBand", new { bandId, fields }),
                    "self",
                    "GET"));
            }

            links.Add(
                new LinkDto(
                Url.Link("DeleteBand", new { bandId }),
                "delete_band",
                "DELETE"));
            links.Add(
                new LinkDto(
                Url.Link("CreateAlbumForBand", new { bandId }),
                "create_album_for_band",
                "POST"));
            links.Add(
              new LinkDto(
              Url.Link("GetAlbumsForBand", new { bandId }),
              "albums",
              "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForBands(BandResourceParameters bandResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    CreateBandsUri(bandResourceParameters, UriType.Current),
                    "self",
                    "GET"
                    )
                );
            if (hasNext)
            {
                links.Add(
               new LinkDto(
                   CreateBandsUri(bandResourceParameters, UriType.NextPage),
                   "nextPage",
                   "GET"
                   )
               );
            }
            if (hasPrevious)
            {
                links.Add(
               new LinkDto(
                   CreateBandsUri(bandResourceParameters, UriType.PreviousPage),
                   "previousPage",
                   "GET"
                   )
               );
            }


            return links;
        }
    }
}
