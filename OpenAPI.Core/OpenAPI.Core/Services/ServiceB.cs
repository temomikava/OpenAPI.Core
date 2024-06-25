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

        public async Task<bool> ProcessAsync(Payment payment)
        {
            await Task.Delay(3000);
            var random = new Random();
            var status = random.Next(2) == 0 ? true : false;
            var statusString = status ? "Completed" : "Rejected";
            logger.LogInformation($"Processed in ServiceB: {payment.CardNumber}, status : {statusString}");
            return status;
        }
    }
}
