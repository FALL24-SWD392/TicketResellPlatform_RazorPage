using Business;
using Repositories.MembershipRepository;
using Repositories.SubscriptionRepository;
using Net.payOS;
using Net.payOS.Types;

namespace Services.PayOSService
{
    public class PayOSService : IPayOSService
    {
        private static PayOSService? instance;
        private readonly IMembershipRepository membershipRepository;
        private readonly ISubscriptionRepository subscriptionRepository;
        private PayOSService() {
            membershipRepository = MembershipRepository.Instance;
            subscriptionRepository = SubscriptionRepository.Instance;
        }
        public static PayOSService Instance
        {
            get
            {
                instance ??= new PayOSService();
                return instance;
            }
        }

        public async Task<Membership> GetPaymentLink(int subscriptionId)
        {
            string clientId = Environment.GetEnvironmentVariable("PAYOS_CLIENT_ID") ?? throw new Exception("PAYOS_CLIENT_ID is not set");
            string apiKey = Environment.GetEnvironmentVariable("PAYOS_API_KEY") ?? throw new Exception("PAYOS_API_KEY is not set");
            string checksumKey = Environment.GetEnvironmentVariable("PAYOS_CHECKSUM_KEY") ?? throw new Exception("PAYOS_API_SECRET is not set");
            PayOS payOS = new(clientId, apiKey, checksumKey);
            var domain = "http://localhost:5153";

            var subscription = await subscriptionRepository.GetAsync(subscriptionId);

            if (subscription == null)
            {
                return null;
            }

            var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: subscription.Price,
                description: "Thanh toan don hang",
                items: [new(subscription.Name, 1, subscription.Price)],
                returnUrl: domain,
                cancelUrl: domain
            );
            var response = await payOS.createPaymentLink(paymentLinkRequest);
            return new Membership
            {
                OrderCode = response.checkoutUrl,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                StatusId = 1,
            };
        }
    }
}
