using Kurs2.Data;
using Kurs2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kurs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public QuotesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }






        // GET: api/<QuotesController>
        [HttpGet]
        [ResponseCache(Duration = 60, Location =ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable quotes;
            switch (sort)
            {
                case "desc":
                    quotes = _dataContext.Quotes.OrderByDescending(x => x.CreatedAt);
                    break;
                case "asc":
                    quotes = _dataContext.Quotes.OrderBy(x => x.CreatedAt);
                    break;

                default:
                    quotes = _dataContext.Quotes;
                    break;
            }

            return Ok(quotes);

        }

        // GET api/Quotes/1
        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(int id)
        {
           var quote= _dataContext.Quotes.Find(id);
            if (quote == null)
                return NotFound();

            return Ok(quote);
        }
        [HttpGet("[action]")]
        public IActionResult MyQuotes()
        {
            var quotes = _dataContext.Quotes.Where(x => x.UserId == GetUserId()).ToList();

            return Ok(quotes);
        }
       
        //api/Quotes/Test/1

        //[HttpGet("[action]/{id}")]
        //public int Test(int id)
        //{
        //    return id;
        //}


        // POST api/<QuotesController>
        [HttpPost]
        public IActionResult Post([FromBody] Quote request)
        {
            string userId = GetUserId();

            request.UserId = userId;
            request.CreatedAt = System.DateTime.Now;
            _dataContext.Quotes.Add(request);
            _dataContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        private string GetUserId()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        // PUT api/<QuotesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote value)
        {
            var quote = _dataContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound();

            }
            if (GetUserId() != quote.UserId)
                return BadRequest("You don't have permission to update this quote!");
            else
            {

                quote.Titile = value.Titile;
                quote.Author = value.Author;
                quote.Descripton = value.Descripton;
                quote.Type = value.Type;

                _dataContext.SaveChanges();

            }
            return Ok();
        }

        // DELETE api/<QuotesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _dataContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound();

            }
            if (GetUserId() != quote.UserId)
                return BadRequest("You don't have permission to delete this quote!");
            _dataContext.Quotes.Remove(quote);
            _dataContext.SaveChanges();

            return Ok("Quote deleted..");
        }


        [HttpGet("[action]")]
   
        //[Route("[action]")]
        public IActionResult Paging(int? pageNumber, int? pageSize)
        {
            var quotes = _dataContext.Quotes.ToList();
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize= pageSize ?? 5;

            return Ok(quotes.Skip((currentPageNumber - 1)* currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public IActionResult Search(string type)
        {
            var quote = _dataContext.Quotes.Where(x => x.Type.StartsWith(type));

            return Ok(quote);
        }
    }
}
