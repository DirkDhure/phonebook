using Autofac;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Services;
using System.Reflection;

namespace PhoneBook.API.Services
{
    public class EventsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .InstancePerLifetimeScope();
            var assembly = Assembly.Load(new AssemblyName("PhoneBook.Core"));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }

    public static class Container
    {
        public static IContainer Resolve()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<EventsModule>();

            return builder.Build();
        }
    }
}
