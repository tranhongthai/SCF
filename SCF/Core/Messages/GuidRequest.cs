using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Messages
{
    public class GuidRequest : ServiceRequest
    {
        public Guid Data { get; set; }

        public override List<ValidationResult> Validate()
        {
            var result = base.Validate();
            result.Add("Data is required", "Data", Data == Guid.Empty);
            return result;
        }

        public override void Init()
        {
            base.Init();
            Data = Guid.Empty;
        }
    }

    public class StringRequest : ServiceRequest
    {
        public string Data { get; set; }

        public override List<ValidationResult> Validate()
        {
            var result = base.Validate();
            result.Add("Data is required", "Data", Data);
            return result;
        }

        public override void Init()
        {
            base.Init();
            Data = string.Empty;
        }
    }
}
