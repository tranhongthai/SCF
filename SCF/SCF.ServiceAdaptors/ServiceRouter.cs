using System.Collections.Generic;
using Peyton.Core;
using Peyton.Core.Messages;
using Peyton.ESB.Domain.Messages;

namespace Peyton.ESB.Domain.ServiceAdaptors
{
    public class PaymentRouter
    {
        public List<IPaymentService> Adaptors { get; set; }
        public static PaymentRouter Deserialize(string applicationId)
        {
            return new PaymentRouter();
        }

        public static string Serialize()
        {
            return "";
        }
        public PaymentResponse Execute(PaymentRequest request)
        {
            var response = new PaymentResponse();
            foreach (var serviceAdaptor in Adaptors)
            {
                response = serviceAdaptor.Pay(request);
                if (response.Result != ServiceResult.Error)
                    break;
            }
            return response;
        }

        public PaymentRouter()
        {
            Adaptors = new List<IPaymentService>();
        }
    }
}
