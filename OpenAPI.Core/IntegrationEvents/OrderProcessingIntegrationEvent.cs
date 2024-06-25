using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents
{
    public class OrderProcessingIntegrationEvent : BaseIntegrationEvent
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CompanyName { get; set; }
    }
}
