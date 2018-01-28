using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("Groups", Schema = "Core")]
    public class Group : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual GroupType Type { get; set; }
        public virtual GroupLevel Level { get; set; }
        public virtual ICollection<RoleProfile> RoleProfiles { get; set; }
    }
}