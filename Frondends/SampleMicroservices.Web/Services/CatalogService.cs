using SampleMicroservices.Shared.Dtos;
using SampleMicroservices.Web.Helper;
using SampleMicroservices.Web.Models;
using SampleMicroservices.Web.Models.Catalogs;
using SampleMicroservices.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;
        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;

        }

        public async Task<bool> CreateCourceAsync(CourseCreateInput courseCreateInput)
        {
            var result = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);

            if (result != null)
            {
                courseCreateInput.Picture = result.Url;
            }
            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("cources", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourceAsync(string courceId)
        {
            var response = await _httpClient.DeleteAsync($"cources/{courceId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourceAsync()
        {
            var response = await _httpClient.GetAsync("cources");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetAllCourceByCourceIdAsync(string courceId)
        {
            var response = await _httpClient.GetAsync($"cources/{courceId}");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            responseSuccess.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);
            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourceByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"cources/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode) { return null; }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourceAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("cources", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
