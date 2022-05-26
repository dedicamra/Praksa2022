using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3._1.Data;
using WebApplication3._1.Dtos.Character;
using WebApplication3._1.Dtos.Weapon;
using WebApplication3._1.Models;

namespace WebApplication3._1.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _db;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;


        public WeaponService(DataContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _db.Characters.FirstOrDefaultAsync(x => x.Id == newWeapon.CharacterId && x.User.Id == GetUserId());
                if(character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;

                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                await _db.Weapons.AddAsync(weapon);
                await _db.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success= false;    
                response.Message=ex.Message;
                
            }

            return response;
        }
    }
}
