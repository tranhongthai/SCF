using Peyton.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCF.ServiceAdaptors.Payment.Messages
{
    public class AccountResponse : ServiceResponse
    {
        public double Balance { get; set; }
        public double AvailableFund { get; set; }
        public AccountResponse()
        {
        }

    }
}
