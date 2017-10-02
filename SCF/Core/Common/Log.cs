using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [Table("Log", Schema = "Core")]
    public class Log : Entity
    {
        public string Key { get; set; }
        public string Message { get; set; }

        public Log()
        {
        }
    }

}
