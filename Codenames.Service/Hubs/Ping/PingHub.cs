using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Codenames.Service.Hubs.Ping
{
    public class PingHub : Hub<IPingClient>
    {
        private readonly IMemoryCache memoryCache;
        public PingHub(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.Pong();

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Ping()
        {
            var pingCountCacheKey = $"{Context.ConnectionId}.PingCount";
            memoryCache.TryGetValue(pingCountCacheKey, out int currentPingCount);
            var newPingCount = currentPingCount++;
            memoryCache.Set(pingCountCacheKey, newPingCount);
            await PingCount(newPingCount);
        }

        public async Task PingCount(int number)
        {
            await Clients.Caller.PingCount(number);
        }

        public async Task Pong()
        {
            await Clients.Caller.Ping();
        }
    }
}
