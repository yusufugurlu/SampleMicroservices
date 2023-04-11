using System.Threading.Tasks;
using System;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
