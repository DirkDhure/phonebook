using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Messaging
{
    public interface ICommandBus
    {
        Task Publish<Td,T>(ICommand<Td> command) where T : ICommand<Td> where Td : ICommandData;

        Task Subscribe<Td,T,H>(H handler) where T : ICommand<Td> where H : ICommandHandler<Td,T> where Td : ICommandData;

    }
}
