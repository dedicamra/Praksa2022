using CoffeeShop.Data;
using CoffeeShop.Models;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public class ReservationService:IReservationService
    {
        private readonly DataContext _db;

        public ReservationService(DataContext db)
        {
            _db = db;
        }

        public async Task AddReservation(Reservation request)
        {
            await _db.Reservations.AddAsync(request);
            await _db.SaveChangesAsync();

            

        }
    }
}
