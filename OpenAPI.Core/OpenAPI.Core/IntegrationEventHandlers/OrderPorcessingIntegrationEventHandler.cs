using IntegrationEvents;
using MassTransit;
using MediatR;
using OpenAPI.Core.Commands;
using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.IntegrationEventHandlers
{
    public class OrderPorcessingIntegrationEventHandler : IIntegrationEventHandler<OrderProcessingIntegrationEvent>
    {
        private readonly ILogger<OrderPorcessingIntegrationEventHandler> logger;
        private readonly IRepository<Payment, int> repository;
        private readonly IMediator mediator;

        public OrderPorcessingIntegrationEventHandler(ILogger<OrderPorcessingIntegrationEventHandler> logger, IRepository<Payment,int> repository, IMediator mediator)
        {
            this.logger = logger;
            this.repository = repository;
            this.mediator = mediator;
        }
        public async Task Consume(ConsumeContext<OrderProcessingIntegrationEvent> context)
        {
            var message = context.Message;
            var payment = new Payment
            {
                OrderId = message.OrderId,
                Amount = message.Amount,
                CardNumber = message.CardNumber,
                CompanyName = message.CompanyName,
                Currency = message.Currency,
                ExpiryDate = message.ExpiryDate
            };
            await repository.CreateAsync(payment);
            await mediator.Send(new ProcessPaymentCommand(payment));
            logger.LogInformation($"Event Consumed Successfully {DateTimeOffset.UtcNow}");
        }
    }
}
