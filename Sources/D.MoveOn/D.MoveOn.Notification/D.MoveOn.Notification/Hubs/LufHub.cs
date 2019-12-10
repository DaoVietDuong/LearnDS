using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace D.MoveOn.Notification.Hubs
{
    public class LufHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var contextId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

    }
}
