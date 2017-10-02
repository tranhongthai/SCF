using Peyton.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class Address
    {
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int? PostCode { get; set; }
        public string Country { get; set; }
        public Address()
        {
            StreetNumber = string.Empty;
            StreetName = string.Empty;
            StreetType = string.Empty;
            Suburb = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            Suburb = string.Empty;
            State = "NSW";
            Country = "AU";
        }
        public Address(Address from)
        {
            from = from ?? new Address();
            StreetNumber = from.StreetNumber;
            StreetName = from.StreetName;
            StreetType = from.StreetType;
            Suburb = from.Suburb;
            AddressLine1 = from.AddressLine1;
            AddressLine2 = from.AddressLine2;
            State = from.State;
            PostCode = from.PostCode;
        }
        public override string ToString()
        {
            return Text;
        }
        public static implicit operator string(Address address)
        {
            return address.Text;
        }
        public string Text
        {
            get
            {
                var address = AddressLine1 + " " +AddressLine2;
                var state = State + " " + PostCode.ToString();
                var text = StringExt.Combine(new string[] { address, Suburb, state, Country }, ", ");
                text = text.Trim();
                return text;
            }
        }
    }

    [Table("State", Schema = "Core")]
    public class State : Entity
    {
        public string Name { get; set; }
        public virtual Country Country { get; set; }
        public State()
        {
            Name = "";
            Country = new Country();
        }
        public State(State from, bool flag): base(from)
        {
            Name = from.Name;
            if (!flag) return;
            if (Country != null)
                Country = new Country(from.Country, flag);
        }
    }

    [Table("Country", Schema = "Core")]
    public class Country : Entity
    {
        public string Name { get; set; }
        public Country()
        {
            Name = "";
        }
        public Country(Country from, bool flag) : base(from)
        {
            Name = from.Name;
        }
    }
}
