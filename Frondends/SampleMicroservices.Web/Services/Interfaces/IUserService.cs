using SampleMicroservices.Web.Models;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
