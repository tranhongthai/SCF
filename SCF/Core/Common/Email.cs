using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class Email : Entity
    {
        public static string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public Email()
        {
            Name = "";
            Address = "";
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            Name = Name ?? "";

            if (Name == "")
                return Address;

            return string.Format("{0} <{1}>", StringExt.Trim(Name), StringExt.Trim(Address));
        }

        public static implicit operator Email(string s)
        {
            var email = new Email();
            email.Address = s;
            return email;
        }

        public static implicit operator string(Email email)
        {
            return email.Address;
        }


    }
}