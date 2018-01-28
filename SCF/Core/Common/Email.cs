using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class Email : Entity
    {
        public Email()
        {
            Name = "";
            Adress = "";
        }

        public string Name { get; set; }
        public string Adress { get; set; }

        public override string ToString()
        {
            Name = Name ?? "";

            if (Name == "")
                return Adress;

            return string.Format("{0} <{1}>", StringExt.Trim(Name), StringExt.Trim(Adress));
        }
    }
}