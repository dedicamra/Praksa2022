using System.Threading.Tasks;
using Test.Dtos.Character;
using Test.Dtos.Weapon;
using Test.Models;

namespace Test.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);

    }
}
