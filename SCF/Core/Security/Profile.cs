using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(10)]
        public string Title { get; set; }
        [StringLength(64)]
        public string FirstName { get; set; }
        [StringLength(64)]
        public string LastName { get; set; }
        [StringLength(64)]
        public string MiddleName { get; set; }
        [StringLength(256)]
        public string PreferredName { get; set; }
        public DateTime? DoB { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RoleProfile> Roles { get; set; }
        public string FullName
        {
            get { return StringExt.Combine(FirstName, LastName); }
        }

        public static Profile GetAnonymous()
        {
            var profile = new Profile();
            profile.LastName = "Anonymous";
            return profile;
        }
    }
}