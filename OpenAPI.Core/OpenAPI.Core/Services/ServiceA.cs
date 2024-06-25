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

        public async Task<bool> ProcessAsync(Payment payment)
        {
            await Task.Delay(3000);
            var random = new Random();
            var status = random.Next(2) == 0 ? true : false;
            var statusString = status ? "Completed" : "Rejected";
            logger.LogInformation($"Processed in ServiceA: {payment.CardNumber}, status : {statusString}");
            return status;
        }
    }
}
