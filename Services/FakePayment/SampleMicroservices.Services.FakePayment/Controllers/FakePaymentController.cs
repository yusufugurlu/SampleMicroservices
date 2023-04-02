using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservices.Shared.ControllerBases;
using SampleMicroservices.Shared.Dtos;

namespace SampleMicroservices.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
