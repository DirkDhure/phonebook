using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Messaging
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
