using Autofac;

namespace D.MoveOn.Common.Dispatchers
{
    public static class Extensions
    {
        public static void AddDispatcherServices(this ContainerBuilder container){

            container.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            container.RegisterType<Dispatcher>().As<IDispatcher>();
        }
    }
}
