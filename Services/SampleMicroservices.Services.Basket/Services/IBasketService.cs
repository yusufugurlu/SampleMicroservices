using SampleMicroservices.Services.Basket.Dtos;
using SampleMicroservices.Shared.Dtos;

namespace SampleMicroservices.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
