using System.Text.Json.Serialization;

namespace PhoneBook.Abstractions.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterType
    {
        And=1,
        Or
    }
}
