using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peyton.Core.Repository
{
    public class Data
    {
        [Key]
        public long id { get; set; }
        public Guid oid { get; set; }

        public Status Status { get; set; }

        public Data()
        {
            oid = Guid.NewGuid();
            Status = Status.New;
        }

        public Data(Data from)
        {
            from = from ?? new Data();
            Clone(from);
        }

        public void Clone(Data from)
        {
            id = from.id;
            oid = from.oid;
            Status = from.Status;
        }
    }
}
