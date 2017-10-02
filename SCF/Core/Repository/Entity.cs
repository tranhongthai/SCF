using System;
using System.ComponentModel.DataAnnotations;

namespace Peyton.Core.Repository
{
    public class Entity : Data
    {
        public Entity()
        {
            Code = string.Empty;
            CreatedTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
            CreatedBy = 0;
            ModifiedBy = 0;
        }

        public Entity(Entity from)
        {
            from = from ?? new Entity();
            Clone(from);
        }

        public void Clone(Entity from)
        {
            base.Clone(from);
            Code = from.Code;
            Sequence = from.Sequence;
            CreatedTime = from.CreatedTime;
            LastModifiedTime = from.LastModifiedTime;
            CreatedBy = from.CreatedBy;
            ModifiedBy = from.ModifiedBy;
        }

        [StringLength(64)]
        public string Code { get; set; }
        public long Sequence { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}