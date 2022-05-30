using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public class MenuService : IMenuService
    {
        private readonly DataContext _db;
        public MenuService(DataContext db)
        {
            _db = db;
        }


        public async Task<List<Menu>> GetMenus()
        {
            var menus = await _db.Menus
                .Include(x=>x.SubMenus)
                .ToListAsync();

            return menus;
        }

        public async Task<Menu> GetById(int id)
        {
            var response = await _db.Menus
                .Include(x=>x.SubMenus)
                .FirstOrDefaultAsync(x => x.Id == id);
            return response;

        }
    }
}
