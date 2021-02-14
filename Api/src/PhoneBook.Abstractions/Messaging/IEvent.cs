using System;

namespace PhoneBook.Abstractions.Messaging
{
    public interface IEvent: IMessage
    {
        Guid EventId { get;  }
        DateTime EventTime { get;  }
      
    }

    public interface IEvent<T> :IEvent, IMessage where T:IEventData
    {
        T EventData { get; set; }
    }
}
