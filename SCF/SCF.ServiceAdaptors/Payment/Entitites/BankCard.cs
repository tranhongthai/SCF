using System;
using System.Text;
using Peyton.Core;
using Peyton.Core.Repository;

namespace SCF.ServiceAdaptors.Payment.Entitites
{
    public class BankCard : Entity
    {
        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public CardType CardType { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CCV { get; set; }
        public AccountType AccountType { get; set; }

        public string ExpiryDate
        {
            get
            {
                var result = ExpiryMonth.ToString("00");
                var year = ExpiryYear%100;
                result += year.ToString("00");
                return result;
            }
        }

        public BankCard()
        {
            HolderName = string.Empty;
            CardNumber = string.Empty;
            ExpiryMonth = DateTime.Today.Month;
            ExpiryYear = DateTime.Today.Year;
            CCV = 0;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Holder Name: " + HolderName);
            builder.Append("Card Number: ****" + CardNumber.Remove(0, CardNumber.Length - 4));
            return builder.ToString();
        }
    }

    public enum AccountType : int
    {
        Cheuque,
        Saving,
        Credit
    }

    public enum CardType : int
    {
        Visa = 0,
        MasterCard,
        Discover,
        AmericanExpress,
        DinersClub,
        JCB
    }
}