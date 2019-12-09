using System.Threading.Tasks;
using D.MoveOn.Common;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
