using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Enterprise;
using Peyton.Core.Repository;
using Peyton.Core.Security;

namespace Peyton.Core.Common
{
    [Table("Application", Schema = "Core")]
    public class Application : Entity
    {
        public Application()
        {
            Name = string.Empty;
            Logo = string.Empty;
            Url = string.Empty;
            Description = string.Empty;
            Overview = string.Empty;
            Version = new Version();
            QoS = new QoS();

            Roles= new List<Role>();
            Specifications = new List<Specification>();
            System = new System();
        }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public Status Status { get; set; }
        public string Color { get; set; }
        public Version Version { get; set; }
        public QoS QoS { get; set; }
        public virtual List<Role> Roles { get; set; }
        public virtual List<Specification> Specifications { get; set; }
        public virtual System System { get; set; }
    }
}