using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using D.MoveOn.Common;
using D.MoveOn.Common.Messages;

namespace D.MoveOn.Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}
