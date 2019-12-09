using System.Threading.Tasks;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _dispatcher;

        public Dispatcher(ICommandDispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
        }
        public Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _dispatcher.SendAsync(command);
        }
    }
}
