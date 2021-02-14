using PhoneBook.Abstractions.Enums;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{

    [DataContract]
    public class ResultMessage
    {
        private readonly string _code;
        private readonly string _message;
        private ResultMessageType _messageType;
        public ResultMessage(ResultMessageType messageType, string code, string message)
        {
            _messageType = messageType;
            _code = code;
            _message = message;
        }

        [DataMember(Name ="messageType")]
        public ResultMessageType MessageType => _messageType;

        [DataMember(Name ="message")]
        public string Message => _message;

        [DataMember(Name ="code")]
        public string Code => _code;
    }
}
