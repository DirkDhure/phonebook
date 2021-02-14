using PhoneBook.Abstractions.Messaging;
using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Events
{
    public interface IEventDispatcher
    {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events"></param>
        /// <returns></returns>
        Task DispatchAsync<T>(params T[] events) where T : IEvent;

    }
}
