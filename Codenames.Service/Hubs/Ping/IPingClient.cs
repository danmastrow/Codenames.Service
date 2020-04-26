using System.Threading.Tasks;

namespace Codenames.Service.Hubs.Ping
{
    public interface IPingClient
    {
        Task Ping();
        Task PingCount(int count);
        Task Pong();
    }
}
