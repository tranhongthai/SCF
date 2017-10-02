using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Security
{
    [Table("User", Schema = "Core")]
    public class User : Entity
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(64)]
        public string Username { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Token AuthenticationToken { get; set; }
        public virtual Token ActivationToken { get; set; }
        public string HashPassword { get; set; }

        public virtual Role Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public LogInResponse LogIn(DbContext context, string username, string password)
        {
            var user = context.Load<User>(i => i.Username == username);
            if (user == null)
                return new LogInResponse("User is not exist");

            if (string.IsNullOrEmpty(password))
                return new LogInResponse("Password is blank");

            if (user.HashPassword != System.StringExt.ToSha1(password))
                return new LogInResponse("Password is incorrect");

            return new LogInResponse() { Result = true };
        }

        public Profile LoadProfile(DbContext context, System.Guid id)
        {
            var user = context.Load<User>(i => i.oid == id);

            if (user == null)
                return Profile.GetAnonymous();

            if (user.Profile == null)
            {
                user.Profile = new Profile();
                return user.Profile;
            }

            return user.Profile;
        }

        public User()
        {
            Name = string.Empty;
            Username = string.Empty;
            HashPassword = string.Empty;
        }

        public User(User from, bool flag = false) : base(from)
        {
            Name = from.Name;
            Username = from.Username;
            HashPassword = from.HashPassword;
        }

    }
}