using OpenAPI.Core.Data;
using SharedKernel;
using System.Diagnostics;

namespace OpenAPI.Core.Services
{
    public class ServiceA : IProcessingService
    {
        private readonly ILogger<ServiceA> logger;

        public ServiceA(ILogger<ServiceA> logger)
        {
            this.logger = logger;
        }

        public void Process(Payment payment)
        {
            logger.LogInformation($"Processed in ServiceA: {payment.CardNumber}");
        }
    }
}
