using SampleMicroservices.Web.Models.Catalogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
	public interface ICatalogService
	{
		Task<List<CategoryViewModel>> GetAllCategoryAsync();
		Task<List<CourseViewModel>> GetAllCourceAsync();
		Task<List<CourseViewModel>> GetAllCourceByUserIdAsync(string userId);

		Task<CourseViewModel> GetAllCourceByCourceIdAsync(string courceId);

		Task<bool> CreateCourceAsync(CourseCreateInput courseCreateInput);

		Task<bool> UpdateCourceAsync(CourseUpdateInput courseUpdateInput);

		Task<bool> DeleteCourceAsync(string courceId);
	}
}
