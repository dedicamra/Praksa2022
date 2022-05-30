using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        public IMenuService _menuService { get; set; }
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetMenus()
        {
            var response = await _menuService.GetMenus();
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var response = await _menuService.GetById(id);
            if (response != null)
                return Ok(response);
            return NotFound();
        }

    }
}
