using System;
using Peyton.Core.Messages;
using SCF.ServiceAdaptors.Payment.Entitites;

namespace SCF.ServiceAdaptors.Payment.Messages
{
    public class RefundRequest : ServiceRequest, IServiceRequest
    {
        public BankCard Card { get; set; }
        public string InvoiceId { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public string Currency { get; set; }

        public RefundRequest()
        {
            Card = new BankCard();
            Amount = 0;
            Comment = string.Empty;
            Currency = "USD";
            InvoiceId = string.Empty;
        }
    }
}