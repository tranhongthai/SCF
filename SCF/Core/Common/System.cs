using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;
using Peyton.Core.Security;

namespace Peyton.Core.Common
{
    [Table("System", Schema = "Core")]
    public class System : Entity
    {
        public System()
        {
            Name = string.Empty;
            Description = string.Empty;
            Applications = new List<Application>();
            Profiles = new List<Profile>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }

    
}