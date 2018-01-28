using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Enterprise;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class QoS : IDisposable
    {
        public QosAttribute Integrity { get; set; }
        public QosAttribute Maintainability { get; set; }
        public QosAttribute Reusability { get; set; }
        public double? Availability { get; set; }
        public QosAttribute Interoperability { get; set; }
        public QosAttribute Manageability { get; set; }
        public PerformanceAttribute Performance { get; set; }
        public QosAttribute Reliability { get; set; }
        public QosAttribute Scalability { get; set; }
        public QosAttribute Security { get; set; }
        public QosAttribute Cost { get; set; }

        public QoS()
        {
            Integrity = new QosAttribute();
            Maintainability = new QosAttribute();
            Reusability = new QosAttribute();
            Interoperability = new QosAttribute();
            Manageability = new QosAttribute();
            Performance = new PerformanceAttribute();
            Reliability = new QosAttribute();
            Scalability = new QosAttribute();
            Security = new QosAttribute();
            Cost = new QosAttribute();
        }

        public void Dispose()
        {
            
        }
    }


    [ComplexType]
    public class PerformanceAttribute
    {
        public string Value { get; set; }
        public long? Throughput { get; set; }
        public double? Latency { get; set; }

        public string ThroughoutText
        {
            get { return StringExt.Format("{0} concurrent users or requests", Throughput); }
        }

        public string LatencyText
        {
            get { return StringExt.Format("{0} seconds", Throughput); }
        }
    }
}
