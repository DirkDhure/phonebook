using Autofac;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Services
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {

            foreach (var @event in events)
            {
                if (@event == null)
                    throw new ArgumentNullException(nameof(@event), "Event can not be null.");



                var eventType = @event.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                object handler;
                _context.TryResolve(handlerType, out handler);

                if (handler == null)
                    return;

                //GetRuntimeMethods() works with .NET Core, otherwise simply use GetMethod()
                var method = handler.GetType()
                    .GetRuntimeMethods()
                    .First(x => x.Name.Equals("HandleAsync"));
                try
                {
                    method.Invoke(handler, new object[] { @event });

                   
                }
                catch (Exception ex)
                {

                }

            }
        }

    }

}

