using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.Services
{
    public class ServiceB : IProcessingService
    {
        private readonly ILogger<ServiceB> logger;

        public ServiceB(ILogger<ServiceB> logger)
        {
            this.logger = logger;
        }

        public void Process(Payment payment)
        {
            logger.LogInformation($"Processed in ServiceB: {payment.CardNumber}");
        }
    }
}
