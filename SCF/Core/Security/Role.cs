using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Common;
using Peyton.Core.Enterprise;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("Roles", Schema = "Core")]
    public class Role : Entity
    {
        public Role()
        {
            Name = string.Empty;
            Description = string.Empty;
            PermissionLevel = 100;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Application Application { get; set; }
        public virtual RoleType Type { get; set; }
        public int PermissionLevel { get; set; }
        public virtual ICollection<RoleProfile> UserRoles { get; set; }
    }
}