using IdentityModel.Client;
using SampleMicroservices.Shared.Dtos;
using SampleMicroservices.Web.Models;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SingIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
