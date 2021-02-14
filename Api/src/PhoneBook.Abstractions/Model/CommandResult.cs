
using PhoneBook.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{
    [DataContract]
    public class CommandResult
    {
        [DataMember(Name = "accepted")]
        public bool Accepted { get; }

        [DataMember(Name = "resourceId")]
        public Guid? ResourceId { get; }

        protected readonly List<ResultMessage> _messages;
        public CommandResult(Guid resourceId, bool accepted)
        {
            ResourceId = resourceId;
            Accepted = accepted;
            _messages = new List<ResultMessage>();
        }

        [DataMember(Name = "messages")]
        public IEnumerable<ResultMessage> Messages => _messages;

        public void AddResultMessage(ResultMessageType messageType, string code, string message)
        {
            _messages.Add(new ResultMessage(messageType, code, message));
        }

    }

    [DataContract]
    public class CommandResult<T> : CommandResult where T :new ()
    {       
        [DataMember(Name = "resource")]
        public T Resource { get; set; }

        public CommandResult(Guid resourceId, T resource, bool accepted) : base(resourceId, accepted)
        {
            Resource = resource;
        }
        
    }
}
