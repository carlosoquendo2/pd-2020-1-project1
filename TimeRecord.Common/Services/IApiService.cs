using System.Threading.Tasks;
using TimeRecord.Common.Models;

namespace TimeRecord.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);
        
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);
        
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RegisterTripAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, TripRequest userRequest);
    }
}
