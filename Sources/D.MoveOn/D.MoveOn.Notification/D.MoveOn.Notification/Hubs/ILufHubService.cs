using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D.MoveOn.Notification.Hubs
{
    public interface ILufHubService
    {
        Task PublishToAllAsync(string message, object data);
    }

    public class LufHubService : ILufHubService
    {
        private readonly IHubContext<LufHub> _hubContext;

        public LufHubService(IHubContext<LufHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task PublishToAllAsync(string message, object data)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message, data);
        }
    }
}
