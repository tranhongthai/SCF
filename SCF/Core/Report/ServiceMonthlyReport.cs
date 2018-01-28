using System;
using System.Collections.Generic;
using System.Linq;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Report
{
    public class ServiceMonthlyReport : ServiceReport
    {
        public ServiceMonthlyReport()
        {
        }

        public override void ExecuteReport(DbContext context)
        {
            var startTime = ReportTime;
            var endTime = startTime.AddMonths(1);
            //var query = context.Get<ServiceDailyReport>(i => i.ReportTime >= startTime && i.ReportTime < endTime);
            var query = context.Get<ServiceDailyReport>();
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