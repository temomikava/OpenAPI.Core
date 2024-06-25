using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.Services
{
    public interface IProcessingServiceFactory
    {
        IProcessingService GetService(Payment payment);
    }
}
