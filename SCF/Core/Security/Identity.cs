using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("Identity", Schema = "Core")]
    public class Identity : Entity
    {
        public Identity()
        {
            Name = string.Empty;
            Username = string.Empty;
        }
        public string Name { get; set; }
        public string Username { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Token AuthenticationToken { get; set; }
        public virtual Token ActivationToken { get; set; }
    }
}