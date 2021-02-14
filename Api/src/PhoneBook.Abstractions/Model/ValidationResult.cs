using System.Collections.Generic;
using System.Linq;
using PhoneBook.Abstractions.Enums;

namespace PhoneBook.Abstractions.Model
{
    public class ValidationResult
    {
        private readonly List<ResultMessage> _messages;
        public ValidationResult()
        {
            _messages = new List<ResultMessage>();
        }
        public bool IsValid
        {
            get
            {
                return ! _messages.Any(m => m.MessageType == ResultMessageType.Error);
            }
        }

        public IEnumerable<ResultMessage> ValidationMessages => _messages;

        public void AddValidationMessage(ResultMessageType validationType,string code, string message)
        {
            _messages.Add(new ResultMessage(validationType, code, message));
        }
    }
}
