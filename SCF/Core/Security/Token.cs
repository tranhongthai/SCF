using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("Token", Schema = "Core")]
    public class Token : Entity
    {
        public DateTime ExpiryTime { get; set; }
        public Token()
        {
            ExpiryTime = DateTime.Now.AddHours(24);
        }
        
    }
}