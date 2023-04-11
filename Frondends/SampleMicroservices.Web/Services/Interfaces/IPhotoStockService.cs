using Microsoft.AspNetCore.Http;
using SampleMicroservices.Web.Models.PhotoStocks;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile photo);

        Task<bool> DeletePhoto(string photoUrl);
    }
}
