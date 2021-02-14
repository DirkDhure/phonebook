using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Messaging
{
    public interface IIntegrationEventHandler<T> where T : IIntegrationEvent
    {
        string HandlerName { get;  }
        Task HandleAsync(T @event);
    }
}
