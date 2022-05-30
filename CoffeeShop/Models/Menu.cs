using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public List<SubMenu> SubMenus{ get; set; }
    }
}
