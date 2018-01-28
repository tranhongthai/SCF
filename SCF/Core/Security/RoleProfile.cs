using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Common;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("ProfileRoles", Schema = "Core")]
    public class RoleProfile : Entity
    {
        public virtual Profile Profile { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
        public virtual UserRoleType Type { get; set; }
        public Guid? EntityId { get; set; }
        public string Note { get; set; }
    }
}