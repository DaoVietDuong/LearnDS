using System;
using D.MoveOn.Common.Exceptions;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.RabbitMQ
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, InternalTicketException, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, InternalTicketException, IRejectedEvent> onError = null)
            where TEvent : IEvent;
    }
}
