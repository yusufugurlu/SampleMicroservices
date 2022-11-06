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
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(result);
        }


        [HttpPost()]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var result = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(result);
        }
    }
}
