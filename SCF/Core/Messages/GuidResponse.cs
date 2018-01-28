using System;

namespace Peyton.Core.Messages
{
    public class GuidResponse : ServiceResponse
    {
        public Guid Data { get; set; }

        public GuidResponse() { }

        public GuidResponse(ServiceRequest request) : base(request) { }

        public override void Init()
        {
            base.Init();
            Data = Guid.Empty;
        }
    }
}
