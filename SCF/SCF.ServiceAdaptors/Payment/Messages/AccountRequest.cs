using Peyton.Core.Messages;


namespace SCF.ServiceAdaptors.Payment.Messages
{
    public class AccountRequest : ServiceRequest, IServiceRequest
    {
        public BankCard Card { get; set; }

        public AccountRequest()
        {
            Card = new BankCard();
        }
    }

    public class AccountResponse : ServiceResponse
    {
        public double Balance { get; set; }
        public double AvailableFund { get; set; }
        public AccountResponse() { }
    }
}