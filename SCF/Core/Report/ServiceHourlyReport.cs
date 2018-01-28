using System;
using System.Collections.Generic;
using System.Linq;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Report
{
    public class ServiceHourlyReport : ServiceReport
    {
        public ServiceHourlyReport()
        {
        }

        public override void ExecuteReport(DbContext context)
        {
            var startTime = ReportTime;
            var endTime = startTime.AddHours(1);
            var query = context.Get<ServiceLog>(i => i.ResponseTime.Start >= startTime && i.ResponseTime.Start < endTime);
            query = query.Where(i => i.Location == Location);
            query = query.Where(i => i.ApplicationCode == ApplicationCode, ApplicationCode);
            var logs = query.ToList();
            ResponseTime = 0;
            Attemps = 0;
            NoFail = 0;
            long maxDuration = 0;
            long failDuration = 0;

            foreach (var log in logs)
            {
                Attemps ++;
                var duration = log.ResponseTime.Duration.Ticks;
                ResponseTime += duration;
                if (duration > maxDuration)
                    maxDuration = duration;
                
                if (log.Result == ServiceResult.Error.ToString())
                {
                    NoFail++;
                    failDuration += duration;
                }
            }

            ServiceCode = maxDuration.ToString();
            Code = failDuration.ToString();
        }
    }
}