using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [Table("Organization", Schema = "Core")]
    public class Organization : Entity
    {
        public Organization()
        {
            Name = string.Empty;
            FullName = string.Empty;
            Description = string.Empty;
            Website = string.Empty;
        }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

    }
}
