using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Messages
{
    public class ServiceRequest : ServiceMessage
    {
        public Guid UserId { get; set; }
        public string ApplicationCode { get; set; }
        public DateTime RequestTime { get; set; }

        public virtual List<ValidationResult> Validate()
        {
            var result = new List<ValidationResult>();
            return result;
        }
        public override void Init()
        {
            base.Init();
            UserId = Guid.Empty;
            ApplicationCode = string.Empty;
            RequestTime = DateTime.Now;
        }
    }
}
