using MessageRouting.Logging;
using Microsoft.Practices.Unity;

namespace MessageRouting.Dependencies
{
    public static class FactoryRegistrations
    {
        public static IUnityContainer RegisterHandlerFactoriesInAssembly <TAssembly>(this IUnityContainer container)
        {
            var assembly = typeof(TAssembly).Assembly;
            var name = assembly.FullName;
            container.RegisterInstance(typeof(IUnityContainer), name, container.CreateChildContainer());
            var child = container.Resolve<IUnityContainer>(name);

            CommandHandlerFactoryMap.Register<TAssembly>(child);
            EventHandlerFactoryMap.Register<TAssembly>(child);

            if (!container.IsRegistered(typeof(ILogMessages)))
            {
                container.RegisterType<ILogMessages, NoLogging>();
            }

            return child;
        }
    }
}