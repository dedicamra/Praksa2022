using CoffeeShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface IMenuService
    {
        Task<List<Menu>> GetMenus();
        Task<Menu> GetById(int id);

    }
}
