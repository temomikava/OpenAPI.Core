using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.Services
{
    public class ProcessingServiceFactory : IProcessingServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProcessingServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IProcessingService GetService(Payment payment)
        {
            var lastDigit = int.Parse(payment.CardNumber.Last().ToString());
            if (lastDigit % 2 == 0)
            {
                return _serviceProvider.GetService<ServiceA>();
            }
            else
            {
                return _serviceProvider.GetService<ServiceB>();
            }
        }
    }
}
