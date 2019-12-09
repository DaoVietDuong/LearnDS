using System.Threading.Tasks;
using D.MoveOn.Common;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.RabbitMQ
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context)
            where TEvent : IEvent;
    }
}
