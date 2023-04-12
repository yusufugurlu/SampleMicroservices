using SampleMicroservices.Web.Models.FakePayments;
using System.Threading.Tasks;

namespace SampleMicroservices.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
