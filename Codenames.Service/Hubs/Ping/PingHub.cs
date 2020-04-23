using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Codenames.Service.Hubs.Ping
{
    public class PingHub : Hub<IPingClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task Ping()
        {
            await Clients.Caller.Pong();
        }

        public async Task Pong()
        {
            await Clients.Caller.Ping();
        }
    }
}
