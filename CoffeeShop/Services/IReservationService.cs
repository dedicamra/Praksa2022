using CoffeeShop.Models;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IReservationService
    {
        Task AddReservation(Reservation request);
    }
}
