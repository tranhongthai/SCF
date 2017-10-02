using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Common
{
    public interface IPersonName
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public static class IPersonNameExt
    {
        public static string GetFullName(this IPersonName obj)
        {
            if (obj == null)
                return string.Empty;
            return StringExt.Combine(obj.FirstName, " ", obj.LastName);
        }
    }
}
