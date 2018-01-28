using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Peyton.Core.Extensions;
using Peyton.Core.Messages;
using Peyton.Core.Report;
using Peyton.Core.Repository;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Common
{
    [Table("Log", Schema = "Core")]
    public class Log : Entity
    {

        public Log()
        {
        }
    }

    public class ServiceLog : Log
    {
        [StringLength(20)]
        public string Name { get; set; }
        public Guid TransactionId { get; set; }
        public string Request { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public DateTimeRange ResponseTime { get; set; }

        [StringLength(20)]
        public string ApplicationCode { get; set; }

        [StringLength(20)]
        public string Location { get; set; }
        public string Note { get; set; }

        [StringLength(20)]
        public string ServiceCode { get; set; }
        public int Attemps { get; set; }
        public int Delay { get; set; }
        public ServiceLogType Type { get; set; }
        public ServiceLog()
        {
            ResponseTime = new DateTimeRange();
            TransactionId = Guid.Empty;
            Request = string.Empty;
            Result = ServiceResult.Success.ToString();
            Message = string.Empty;
            Note = string.Empty;
            ServiceCode = string.Empty;
            Type = ServiceLogType.CloudService;
        }

        public ServiceLog(ServiceRequest request)
        {
            ResponseTime = new DateTimeRange();
            Request = string.Empty;
            Result = ServiceResult.Success.ToString();
            Message = string.Empty;
            Note = string.Empty;
            ServiceCode = string.Empty;
            Type = ServiceLogType.CloudService;

            TransactionId = request.TransactionId;
            Request = request.ToString();
            ApplicationCode = request.ApplicationCode;
        }

    }

   

    
}
