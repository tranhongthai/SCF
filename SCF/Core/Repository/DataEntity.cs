using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peyton.Core.Common;

namespace Peyton.Core.Repository
{
    public class DataEntity: Entity
    {
        public virtual Organization Organization { get; set; }
        public virtual Application Application { get; set; }
    }
}
