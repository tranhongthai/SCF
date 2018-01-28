using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Security
{
    [Table("Profile", Schema = "Core")]
    public class Profile : Entity
    {
        public Profile()
        {
            Title = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = string.Empty;
        }
        public string HashPassword { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PreferredName { get; set; }
        public DateTime? DoB { get; set; }
        public virtual ICollection<Identity> Identities { get; set; }
        public virtual ICollection<RoleProfile> Roles { get; set; }
        public bool Aboriginal { get; set; }
        public bool TorresStaitIslander { get; set; }
        public virtual ICollection<Common.System> Systems { get; set; }

        public string FullName
        {
            get { return StringExt.Combine(FirstName, LastName); }
        }
    }
}