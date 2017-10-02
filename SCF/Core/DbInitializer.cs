using Peyton.Core.Security;
using System;
using Peyton.Core.Repository;
using Peyton.Core.Common;

namespace Peyton.Core
{
    public class DbInitializer 
    {
        public static void Seed(DbContext context)
        {
            SeedRole(context);
            SeedUser(context);
            //SeedCountry(context);
            //SeedState(context);
        }

        private static void SeedRole(DbContext context)
        {
            var systemAdmin = new Role();
            systemAdmin.Code = "systemadmin";
            systemAdmin.Name = "System Administrator";
            systemAdmin.PermissionLevel = 100;
            context.Add(systemAdmin);

            var admin = new Role();
            admin.Code = "admin";
            admin.Name = "Administrator";
            admin.PermissionLevel = 80;
            context.Add(admin);

            var manager = new Role();
            manager.Code = "manager";
            manager.Name = "Managers";
            manager.PermissionLevel = 60;
            context.Add(manager);

            var advanceduser = new Role();
            advanceduser.Code = "advanceduser";
            advanceduser.Name = "Advanced User";
            advanceduser.PermissionLevel = 20;
            context.Add(advanceduser);

            var user = new Role();
            user.Code = "user";
            user.Name = "User";
            user.PermissionLevel = 10;
            context.Add(user);
            context.SaveChanges();

        }

        private static void SeedUser(DbContext context)
        {
            var profile = new Profile();
            profile.Code = "sa";
            profile.FirstName = "System";
            profile.LastName = "Administrator";
            profile.PreferredName = "Admin";

            
            var user = new User();
            user.Name = "Admin";
            user.Status = Status.Normal;
            user.Code = "sa";
            user.Username = "tranhongthai@gmail.com";
            user.HashPassword = "123456".ToSha1();
            user.Profile = profile;
            user.Role = context.Load<Role>("systemadmin");
            context.Add(user);
            context.SaveChanges();
        }

        private static void SeedCountry(DbContext context)
        {
            var australia = new Country();
            australia.Code = "AU";
            australia.Name = "Australia";
            context.Add(australia);
            context.SaveChanges();
        }

        private static void SeedState(DbContext context)
        {
            var au = context.Load<Country>("AU");

            var nsw = new State();
            nsw.Code = "NSW";
            nsw.Name = "New South Wales";
            nsw.Country = au;
            context.Add(nsw);

            var qld = new State();
            qld.Code = "QLD";
            qld.Name = "Queenlands";
            qld.Country = au;
            context.Add(qld);
            context.SaveChanges();
        }
    }
}