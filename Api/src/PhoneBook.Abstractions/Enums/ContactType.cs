using System.Text.Json.Serialization;

namespace PhoneBook.Abstractions.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactType
    {
        Mobile =1,
        Home= 2,
        Email= 3,
        Work=4
    }
}
