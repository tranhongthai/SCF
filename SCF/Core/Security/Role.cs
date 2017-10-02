using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string Description { get; set; }
        public int PermissionLevel { get; set; }
        public virtual ICollection<RoleProfile> UserRoles { get; set; }
    }
}