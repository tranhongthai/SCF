using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peyton.Core;
using SCF.ServiceAdaptors;
using SCF.ServiceAdaptors.Payment;
using SCF.ServiceAdaptors.Payment.Messages;

namespace SCF.WorkflowEngine
{
    public class PaymentWorkflow : IPaymentService
    {
        private int _active = 0;
        public List<IPaymentService> Services { get; set; }
        public WorkflowMode Mode { get; set; }

        public PaymentWorkflow()
        {
            Services = new List<IPaymentService>();
            Mode = WorkflowMode.Failover;
        }

        public PaymentResponse Pay(PaymentRequest request)
        {
            var n = Services.Count;
            var response = new PaymentResponse();

            if (n == 0)
                throw new Exception("There is no service adaptors");

            if (Mode == WorkflowMode.LoadBalance)
            {
                response = Services[_active].Pay(request);
                _active = (_active + 1) % n;
                return response;
            }

            foreach (var service in Services)
            {
                response = service.Pay(request);
                if (response.Result != ServiceResult.Error)
                    break;
            }
            return response;
        }

        public RefundResponse Refund(RefundRequest request)
        {
            throw new NotImplementedException();
        }

        public AccountResponse Check(AccountRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public enum WorkflowMode
    {
        Failover,
        LoadBalance,
    }
}
