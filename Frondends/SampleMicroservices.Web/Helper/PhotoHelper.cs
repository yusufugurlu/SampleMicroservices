using Microsoft.Extensions.Options;
using SampleMicroservices.Web.Models;

namespace SampleMicroservices.Web.Helper
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            this._serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string url)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/{url}";
        }
    }
}
