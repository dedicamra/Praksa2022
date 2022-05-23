using System.Text.Json.Serialization;

namespace Test.Models
{
    [JsonConverter (typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}
