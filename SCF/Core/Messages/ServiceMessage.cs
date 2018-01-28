using System;

namespace Peyton.Core.Messages
{
    public class ServiceMessage
    {
        public Guid TransactionId { get; set; }

        public ServiceMessage()
        {
            Init();
        }

        public virtual void Init()
        {
            TransactionId = Guid.NewGuid();
        }

        
    }
}
