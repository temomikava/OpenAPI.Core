using IntegrationEvents;
using MassTransit;
using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.IntegrationEventHandlers
{
    public class OrderPorcessingIntegrationEventHandler : IIntegrationEventHandler<OrderProcessingIntegrationEvent>
    {
        private readonly ILogger<OrderPorcessingIntegrationEventHandler> logger;
        private readonly IRepository<Payment, int> repository;

        public OrderPorcessingIntegrationEventHandler(ILogger<OrderPorcessingIntegrationEventHandler> logger, IRepository<Payment,int> repository)
        {
            this.logger = logger;
            this.repository = repository;
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
            logger.LogInformation($"Event Consumed Successfully {DateTimeOffset.UtcNow}");
        }
    }
}
