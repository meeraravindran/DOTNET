using System.Text.Json.Serialization;

namespace Webapi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        King,
        Knight,
        Hand_of_the_King,
        Queen

    }
}
