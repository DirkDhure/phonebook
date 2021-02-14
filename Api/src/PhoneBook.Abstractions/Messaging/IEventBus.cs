using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Messaging
{
    public interface IEventBus
    {
        Task Publish(IIntegrationEvent @event);
        Task Subscribe<T, TH>() where T : IIntegrationEvent where TH : IIntegrationEventHandler<T>;
        Task Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IIntegrationEvent;
        Task SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;
        Task UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler;

    }
}
