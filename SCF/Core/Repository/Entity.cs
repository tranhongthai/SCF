using System;

namespace Peyton.Core.Repository
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            Code = string.Empty;
            No = 0;
            CreatedTime = DateTime.Now;
            LastModifiedTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public long No { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}