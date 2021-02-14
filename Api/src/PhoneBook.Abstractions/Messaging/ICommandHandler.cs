using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Messaging
{
    public interface ICommandHandler<Td, T> where T : ICommand<Td> where Td : ICommandData
    {
        string HandlerName { get; }
        Task HandleAsync(T command);
    }

}
