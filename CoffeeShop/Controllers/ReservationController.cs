using CoffeeShop.Data;
using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reservation request)
        {
            await _reservationService.AddReservation(request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
