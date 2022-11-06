using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMicroservices.Services.Catalog.Dtos;
using SampleMicroservices.Services.Catalog.Services;
using SampleMicroservices.Shared.ControllerBases;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourcesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CourcesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _courseService.GetAllAsync();
            return CreateActionResultInstance(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }

        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var result = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CourseCreateDto courseDto)
        {
            var result = await _courseService.CreateAsync(courseDto);
            return CreateActionResultInstance(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(CourseUpdateDto courseDto)
        {
            var result = await _courseService.UpdateAsync(courseDto);
            return CreateActionResultInstance(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(result);
        }
    }
}
