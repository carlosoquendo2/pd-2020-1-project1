using System.Threading.Tasks;
using TimeRecord.Common.Models;

namespace TimeRecord.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
    }
}
