using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservices.Services.Basket.Dtos;
using SampleMicroservices.Services.Basket.Services;
using SampleMicroservices.Shared.ControllerBases;
using SampleMicroservices.Shared.Services;

namespace SampleMicroservices.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            string userId = _sharedIdentityService.GetUserId;
            var data = await _basketService.GetBasket(userId);
            return CreateActionResultInstance(data);
        }


        [HttpPost]
        public async Task<IActionResult> SaveOrUpdteBasket(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto); ;
            return CreateActionResultInstance(response);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            string userId = _sharedIdentityService.GetUserId;
            return CreateActionResultInstance(await _basketService.Delete(userId));
        }
    }
}
