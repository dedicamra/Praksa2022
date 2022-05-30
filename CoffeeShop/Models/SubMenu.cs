using System.Text.Json.Serialization;

namespace CoffeeShop.Models
{
    public class SubMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public  Menu Menu { get; set; }
        
    }
}
