using System.Threading.Tasks;
using Consul;

namespace D.MoveOn.Common.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}