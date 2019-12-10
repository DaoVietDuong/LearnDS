using D.MoveOn.Common;
using D.MoveOn.Common.Handlers;
using D.MoveOn.Common.Messages;
using D.MoveOn.Notification.Hubs;
using D.MoveOn.Notification.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Notification.Handlers
{
    public class NotifyHandler: IEventHandler<ResultProcess>
    {
        private readonly ILufHubService _lufHubService;

        public NotifyHandler(ILufHubService lufHubService)
        {
            _lufHubService = lufHubService;
        }

        public async Task HandleAsync(ResultProcess @event, ICorrelationContext context)
        {
            await _lufHubService.PublishToAllAsync(@event.Resource, new { id = @event.Id, name = @event.Name });
        }
    }
}
