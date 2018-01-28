using SCF.ServiceAdaptors.Payment.Messages;

namespace SCF.ServiceAdaptors.Payment
{
    public interface IPaymentService : ICloudService
    {
        PaymentResponse Pay(PaymentRequest request);

        RefundResponse Refund(RefundRequest request);

        AccountResponse Check(AccountRequest request);
    }
}
