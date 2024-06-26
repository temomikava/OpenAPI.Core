﻿using MediatR;
using OpenAPI.Core.Data;

namespace OpenAPI.Core.Commands
{
    public class ProcessPaymentCommand : IRequest<bool>
    {

        public ProcessPaymentCommand(Payment payment)
        {
            Payment = payment;
        }

        public Payment Payment { get; }
    }
}
