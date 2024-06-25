using OpenAPI.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public interface IProcessingService
    {
        Task<bool> ProcessAsync(Payment payment);
    }
}
