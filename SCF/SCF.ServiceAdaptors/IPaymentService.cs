using SCF.ServiceAdaptors.Payment.Messages;

namespace SCF.ServiceAdaptors
{
    public interface IPaymentService : IServiceAdaptor
    {
        PaymentResponse Pay(PaymentRequest request);

        RefundResponse Refund(RefundRequest request);

        AccountResponse Check(AccountRequest request);
    }
}
