using System.Threading.Tasks;

namespace D.MoveOn.Common.Fabio
{
    public interface IFabioHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}