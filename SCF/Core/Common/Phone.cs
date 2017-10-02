using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class Phone
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string Ext { get; set; }

        public Phone()
        {
            Type = string.Empty;
            Number = string.Empty;
            Ext = string.Empty;
        }

        public static implicit operator Phone(string s)
        {
            var phone = new Phone();
            phone.Number = s;
            return phone;
        }

        public static implicit operator string(Phone phone)
        {
            return phone.Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
