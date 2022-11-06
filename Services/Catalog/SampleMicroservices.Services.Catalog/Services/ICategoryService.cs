using SampleMicroservices.Services.Catalog.Dtos;
using SampleMicroservices.Services.Catalog.Models;
using SampleMicroservices.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    }
}
