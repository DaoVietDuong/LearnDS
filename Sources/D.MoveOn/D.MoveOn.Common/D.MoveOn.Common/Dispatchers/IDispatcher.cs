using System.Threading.Tasks;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
