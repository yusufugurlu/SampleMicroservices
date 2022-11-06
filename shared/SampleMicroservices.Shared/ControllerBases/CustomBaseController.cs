using Microsoft.AspNetCore.Mvc;
using SampleMicroservices.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMicroservices.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {

        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
