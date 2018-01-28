using Peyton.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Enterprise
{
    public class QoSDescription
    {
        public string Integrity { get; set; }
        public string Maintainability { get; set; }
        public string Reusability { get; set; }
        public string Availability { get; set; }
        public string Interoperability { get; set; }
        public string Manageability { get; set; }
        public string Performance { get; set; }
        public string Reliability { get; set; }
        public string Scalability { get; set; }
        public string Security { get; set; }
        public string Cost { get; set; }

        public QoSDescription()
        {
            Integrity = "Conceptual integrity defines the consistency and coherence of the overall design. This includes the way that components or modules are designed, as well as factors such as coding style and variable naming.";
            Maintainability = "Maintainability is the ability of the system to undergo changes with a degree of ease. These changes could impact components, services, features, and interfaces when adding or changing the functionality, fixing errors, and meeting new business requirements.";
            Reusability = "Reusability defines the capability for components and subsystems to be suitable for use in other applications and in other scenarios. Reusability minimizes the duplication of components and also the implementation time.";
            Availability = "Availability defines the proportion of time that the system is functional and working. It can be measured as a percentage of the total system downtime over a predefined period. Availability will be affected by system errors, infrastructure problems, malicious attacks, and system load.";
            Interoperability = "Interoperability is the ability of a system or different systems to operate successfully by communicating and exchanging information with other external systems written and run by external parties. An interoperable system makes it easier to exchange and reuse information internally as well as externally.";
            Manageability = "Manageability defines how easy it is for system administrators to manage the application, usually through sufficient and useful instrumentation exposed for use in monitoring systems and for debugging and performance tuning.";
            Performance = "Performance is an indication of the responsiveness of a system to execute any action within a given time interval. It can be measured in terms of latency or throughput. Latency is the time taken to respond to any event. Throughput is the number of events that take place within a given amount of time.";
            Reliability = "Reliability is the ability of a system to remain operational over time. Reliability is measured as the probability that a system will not fail to perform its intended functions over a specified time interval.";
            Scalability = "Scalability is ability of a system to either handle increases in load without impact on the performance of the system, or the ability to be readily enlarged.";
            Security = "Security is the capability of a system to prevent malicious or accidental actions outside of the designed usage, and to prevent disclosure or loss of information. A secure system aims to protect assets and prevent unauthorized modification of information.";
            Cost = "";
        }
    }
}
