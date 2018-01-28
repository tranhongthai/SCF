using System;
using System.Collections.Generic;
using System.Linq;
using Peyton.Core.Repository;

namespace Peyton.Core.Report
{
    public class ServiceDailyReport : ServiceReport
    {
        public ServiceDailyReport()
        {
        }

        public override void ExecuteReport(DbContext context)
        {
            var startTime = ReportTime;
            var endTime = startTime.AddDays(1);
            var query = context.Get<ServiceHourlyReport>(i => i.ReportTime >= startTime && i.ReportTime < endTime);
            query = query.Where(i => i.ServiceCode == ServiceCode);
            query = query.Where(i => i.ApplicationCode == ApplicationCode);
            query = query.Where(i => i.ServiceType == ServiceType);
            var reports = query.ToList();
            ResponseTime = 0;
            NoFail = Attemps = 0;
            foreach (var report in reports)
            {
                NoFail += report.NoFail;
                Attemps += report.Attemps;
                ResponseTime += report.ResponseTime;
            }
        }
    }
}