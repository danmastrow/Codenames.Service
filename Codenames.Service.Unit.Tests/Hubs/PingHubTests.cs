using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Codenames.Service.Hubs.Ping;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;

namespace Codenames.Service.Unit.Tests.Hubs
{
    public class PingHubTests
    {
        [Fact]
        public async Task SendingPingShouldSendPongToCaller()
        {
            var pingHub = new PingHub();
            Mock<IHubCallerClients<IPingClient>> mockClients = new Mock<IHubCallerClients<IPingClient>>();
            Mock<IPingClient> mockClientProxy = new Mock<IPingClient>();
            Mock<HubCallerContext> mockClientContext = new Mock<HubCallerContext>();
            mockClients.Setup(clients => clients.Caller).Returns(mockClientProxy.Object);
            mockClientContext.Setup(c => c.ConnectionId).Returns(Guid.NewGuid().ToString);
            pingHub.Clients = mockClients.Object;
            pingHub.Context = mockClientContext.Object;
            await pingHub.Ping();

            mockClientProxy.Verify(c => c.Pong(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task SendingPongShouldSendPingToCaller()
        {
            var pingHub = new PingHub();
            Mock<IHubCallerClients<IPingClient>> mockClients = new Mock<IHubCallerClients<IPingClient>>();
            Mock<IPingClient> mockClientProxy = new Mock<IPingClient>();
            Mock<HubCallerContext> mockClientContext = new Mock<HubCallerContext>();
            mockClients.Setup(clients => clients.Caller).Returns(mockClientProxy.Object);
            mockClientContext.Setup(c => c.ConnectionId).Returns(Guid.NewGuid().ToString);
            pingHub.Clients = mockClients.Object;
            pingHub.Context = mockClientContext.Object;
            await pingHub.Pong();

            mockClientProxy.Verify(c => c.Ping(), Times.AtLeastOnce);
        }
    }
}
