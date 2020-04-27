using System;
using System.Threading.Tasks;
using Codenames.Service.Hubs.Ping;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Codenames.Service.Unit.Tests.Hubs
{
    public class PingHubTests
    {


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(33)]
        public async Task SendingPingShouldSendPingCountToCaller(int currentPingCount)
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            var pingHub = new PingHub(memoryCache);
            var mockClients = new Mock<IHubCallerClients<IPingClient>>();
            var mockClientProxy = new Mock<IPingClient>();
            var mockClientContext = new Mock<HubCallerContext>();
         
            pingHub.Clients = mockClients.Object;
            pingHub.Context = mockClientContext.Object;
            
            mockClients.Setup(clients => clients.Caller).Returns(mockClientProxy.Object);
            mockClientContext.Setup(c => c.ConnectionId).Returns(Guid.NewGuid().ToString);

            var connectionId = mockClientContext.Object.ConnectionId;
            var cacheKey = pingHub.PingCountCacheKey(connectionId);
            memoryCache.Set(cacheKey, currentPingCount);

            await pingHub.Ping();

            mockClientProxy.Verify(c => c.PingCount(currentPingCount + 1), Times.Once);
        }


    }
}
