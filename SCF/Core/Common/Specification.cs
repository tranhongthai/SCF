using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [Table("Specification", Schema = "Core")]
    public class Specification : Entity
    {
        public Specification()
        {
            Name = string.Empty;
            Status = Status.Normal;
            Priority = Priority.Medium;
            Description = string.Empty;
            Detail = string.Empty;
            Importance = Severity.Normal;
            Application = new Application();
        }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public Severity Importance { get; set; }
        public virtual Application Application { get; set; }
    }
}