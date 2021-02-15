using PhoneBook.Abstractions.Messaging;
using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Events
{
    public interface IEventDispatcher
    {

        Task DispatchAsync<T>(params T[] events) where T : IEvent;

    }
}
