using System;
using System.Collections.Generic;
using Peyton.Core.Common;

namespace Peyton.Core.Messages
{
    public interface IServiceResponse
    {
        ServiceResult Result { get; set; }
        Guid TransactionId { get; set; }
        string Message { get; set; }
        List<ValidationModel> Errors { get; set; }
    }
}