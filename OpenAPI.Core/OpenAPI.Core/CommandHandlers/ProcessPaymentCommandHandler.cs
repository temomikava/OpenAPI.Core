using MediatR;
using OpenAPI.Core.Commands;
using OpenAPI.Core.Services;

namespace OpenAPI.Core.CommandHandlers
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand>
    {
        private readonly IProcessingServiceFactory _processingServiceFactory;

        public ProcessPaymentCommandHandler(IProcessingServiceFactory processingServiceFactory)
        {
            _processingServiceFactory = processingServiceFactory;
        }
        public Task<Unit> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var processingService = _processingServiceFactory.GetService(request.Payment);
            processingService.Process(request.Payment);
            return Task.FromResult(Unit.Value);
        }
    }
}
