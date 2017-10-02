using Peyton.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Security.Models
{
    public class UserModel : Model
    {
        public string Name { get; set; }

        public string Role { get; set; }
    }
}
