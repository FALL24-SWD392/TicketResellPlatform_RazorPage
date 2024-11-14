using Business;

namespace Services.PayOSService
{
    public interface IPayOSService
    {
        Task<Membership> GetPaymentLink(int subscriptionId);
    }
}
