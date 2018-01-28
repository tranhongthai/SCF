using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Report
{
    [Table("ServiceReport", Schema = "Report")]
    public class ServiceReport : Entity
    {
        public string Name { get; set; }
        public long ResponseTime { get; set; }
        public double NoFail { get; set; }
        public DateTime ReportTime { get; set; }
        public string ServiceCode { get; set; }
        public string ApplicationCode { get; set; }
        public string Location { get; set; }
        public string ServiceType { get; set; }
        public double Attemps { get; set; }

        public double GetAvailability()
        {
            if (Attemps == 0)
                return 0;
            var pf = 1 - NoFail / Attemps;
            var pf1 = (int)(pf * 1000000);
            return pf1 / 10000.0;
        }

        public double GetResponseTime()
        {
            if (Attemps == 0)
                return 0;
            var time = new TimeSpan(ResponseTime);
            var value = time.TotalSeconds / Attemps;
            var round = (int) (value*100);
            return round/100.0;
        }

        public double GetSuccessResponseTime()
        {
            if (Attemps == 0)
                return 0;
            var failTime = Code.ToLong();
            var reponseTime = ResponseTime - failTime;
            var time = new TimeSpan(reponseTime);
            var value = time.TotalSeconds / (Attemps - NoFail);
            var round = (int)(value * 100);
            return round / 100.0;
        }

        public double GetFailResponseTime()
        {
            if (NoFail == 0)
                return 0;
            var failTime = Code.ToLong();
            var time = new TimeSpan(failTime);
            var value = time.TotalSeconds / NoFail;
            var round = (int)(value * 100);
            return round / 100.0;
        }

        public double GetMaxResponseTime()
        {
            if (Attemps == 0)
                return 0;
            var time = new TimeSpan(ServiceCode.ToLong());
            var value = time.TotalSeconds;
            var round = (int)(value * 100);
            return round / 100.0;
        }
        public ServiceReport()
        {
            Name = string.Empty;
            ResponseTime = 0;
            NoFail = 0;
            ReportTime = DateTime.Today;
            ServiceCode = string.Empty;
            ApplicationCode = string.Empty;
            Location = string.Empty;
            ServiceType = string.Empty;
            Attemps = 0;
        }

        public virtual void ExecuteReport(DbContext context)
        {
            
        }

        public virtual void GetReports(DbContext context)
        {

        }
    }
}