using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Common
{
    public class Model
    {
        public Guid oid { get; set; }
        public long id { get; set; }
        public Model()
        {
            oid = Guid.NewGuid();
        }
    }
}
