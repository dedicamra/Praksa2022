using System.Threading.Tasks;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Dtos.Weapon;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);

    }
}
