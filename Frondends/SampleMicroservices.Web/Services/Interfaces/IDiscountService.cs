using SampleMicroservices.Web.Models.Discounts;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
