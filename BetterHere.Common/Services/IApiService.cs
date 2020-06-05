using BetterHere.Common.Models;
using System.Threading.Tasks;

namespace BetterHere.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, FacebookProfile request);
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<Response> GetEstablishmentAsync(string urlBase, string servicePrefix, string controller);

        Task<Response> GetEstablishmentDetailAsync(string name, string urlBase, string servicePrefix, string controller);

        Task<Response> NewEstablishmentAsync(string urlBase, string servicePrefix, string controller, EstablishmentRequest model, string tokenType, string accessToken);

        Task<Response> GetEstablishmentLocationAsync(string urlBase, string servicePrefix, string controller, EstablishmentRequest model);

        Task<Response> NewEstablishmentLocationAsync(string urlBase, string servicePrefix, string controller, EstablishmentLocationRequest model, string tokenType, string accessToken);

        Task<Response> GetFoodAsync(string urlBase, string servicePrefix, string controller, FoodRequest model);

        Task<Response> NewFoodAsync(string urlBase, string servicePrefix, string controller, FoodRequest model, string tokenType, string accessToken);
    }
}
