

using Peyton.Core.Messages;
using SCF.ServiceAdaptors.Payment.Entitites;

namespace SCF.ServiceAdaptors.Payment.Messages
{
    public class AccountRequest : ServiceRequest, IServiceRequest
    {
        public BankCard Card { get; set; }
        public string InvoiceId { get; set; }

        public AccountRequest()
        {
            Card = new BankCard();
        }
    }
}