using System.Threading.Tasks;

namespace D.MoveOn.Common.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}