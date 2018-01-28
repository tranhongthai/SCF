using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class QosAttribute
    {
        public string Value { get; set; }
        public double? Index { get; set; }
        public QosAttribute()
        {
            Value = string.Empty;
        }

        public override string ToString()
        {
            return Value != string.Empty ? Value : StringExt.Format("{0}", Index);
        }

        public bool HasValue
        {
            get { return Value != string.Empty || Index.HasValue; }
        }
    }
}