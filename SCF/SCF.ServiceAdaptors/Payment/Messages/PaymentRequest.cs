
using Peyton.Core.Messages;
using SCF.ServiceAdaptors.Payment.Entitites;
using System.Text;


namespace SCF.ServiceAdaptors.Payment.Messages
{
    public class PaymentRequest : ServiceRequest, IServiceRequest
    {
        public BankCard Card { get; set; }
        public string InvoiceId { get; set; }
        public double Amount { get; set; }
        public double? Amount2 { get; set; }
        public double? Amount3 { get; set; }
        public string Comment { get; set; }
        public string Currency { get; set; }
        public string Location { get; set; }

        public PaymentRequest()
        {
            Card = new BankCard();
            Amount = 0;
            Comment = string.Empty;
            Currency = "USD";
            InvoiceId = string.Empty;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Location);
            builder.AppendLine(Card.ToString());
            builder.AppendLine("Amount: " + Amount + Currency);
            if (Amount2.HasValue)
                builder.AppendLine("Amount 2: " + Amount2.Value + "" + Currency);
            if (Amount3.HasValue)
                builder.AppendLine("Amount 3: " + Amount3.Value + "" + Currency);
            return builder.ToString();
        }
    }
}
