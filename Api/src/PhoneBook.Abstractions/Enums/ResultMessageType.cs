using System.Text.Json.Serialization;

namespace PhoneBook.Abstractions.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResultMessageType
    {
        Information = 1,
        Warning = 2,
        Error = 3
    }
}
