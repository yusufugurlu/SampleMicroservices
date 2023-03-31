using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservices.Services.Photo.Dtos;
using SampleMicroservices.Shared.ControllerBases;
using SampleMicroservices.Shared.Dtos;
using System.IO;

namespace SampleMicroservices.Services.Photo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : CustomBaseController
    {
      

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));

            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("Pgoro is empty", 400));
        }

        [HttpPut]
        public IActionResult PhotoDelete(PhotoDto photoDto)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoDto.Url);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));

        }
        
    }
}
