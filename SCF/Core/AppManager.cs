using Peyton.Core.Repository;
using Peyton.Core.Security;
using Peyton.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Peyton.Core
{
    public class AppManager
    {
        public static string ApplicationType
        {
            get
            {
                return SettingManager.Get<string>("ApplicationType");
            }
        }
        public static User CurrentUser
        {
            get
            {
                if (ApplicationType == "Web" && SessionManager.Exist("CurrentUser"))
                        return SessionManager.Get<User>("CurrentUser");

                var user = new User();
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return user;

                var id = HttpContext.Current.User.Identity.Name;

                using (var context = new DbContext())
                {
                    var data = context.Load<User>(i => i.Username == id);
                    if (data == null)
                        FormsAuthentication.SignOut();
                    else
                    {
                        user.id = data.id;
                        user.oid = data.oid;
                        user.Name = data.Name;
                        user.Username = data.Username;
                        SessionManager.Set("CurrentUser", user);
                    }
                }
                return user;
            }
        }
    }
}
