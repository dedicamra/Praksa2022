using System.Text.Json.Serialization;

namespace WebApplication3._1.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}
