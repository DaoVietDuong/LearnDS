using System.Threading.Tasks;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
