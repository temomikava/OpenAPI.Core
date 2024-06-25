using IntegrationEvents;
using MediatR;
using OpenAPI.Core.Commands;
using OpenAPI.Core.Data;
using OpenAPI.Core.Services;
using SharedKernel;

namespace OpenAPI.Core.CommandHandlers
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, bool>
    {
        private readonly IProcessingServiceFactory _processingServiceFactory;
        private readonly IIntegrationEventService integrationEventService;
        private readonly IRepository<Payment, int> repository;

        public ProcessPaymentCommandHandler(IProcessingServiceFactory processingServiceFactory, IIntegrationEventService integrationEventService, IRepository<Payment, int> repository)
        {
            _processingServiceFactory = processingServiceFactory;
            this.integrationEventService = integrationEventService;
            this.repository = repository;
        }
        public async Task<bool> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var processingService = _processingServiceFactory.GetService(request.Payment);
            var completed = await processingService.ProcessAsync(request.Payment);
            
            if (completed)
            {
                await integrationEventService.AddEventAsync(new OrderCompletedIntegrationEvent(request.Payment.OrderId));
                await integrationEventService.PublishEventsAsync(Guid.NewGuid(), cancellationToken);
                request.Payment.Status = PaymentStatus.Completed;
            }
            else
            {
                await integrationEventService.AddEventAsync(new OrderRejectedIntegrationEvent(request.Payment.OrderId));
                await integrationEventService.PublishEventsAsync(Guid.NewGuid(), cancellationToken);
                request.Payment.Status = PaymentStatus.Rejected;
            }
            await repository.UpdateAsync(request.Payment);
            return completed;
        }
    }
}
