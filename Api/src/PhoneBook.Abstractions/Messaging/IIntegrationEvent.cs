namespace PhoneBook.Abstractions.Messaging
{
    public interface IIntegrationEvent : IEvent
    {
        string SystemId { get; set; }
        string SystemName { get; set; }
    }
}
