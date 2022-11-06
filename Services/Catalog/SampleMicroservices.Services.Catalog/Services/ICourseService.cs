using SampleMicroservices.Services.Catalog.Dtos;
using SampleMicroservices.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
