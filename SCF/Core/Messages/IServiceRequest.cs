using System;

namespace Peyton.Core.Messages
{
    public interface IServiceRequest
    {
        DateTime RequestTime { get; set; }
    }
}