namespace Peyton.Core.Repository
{
    public class EnumEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EnumEntity()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            if (Description != string.Empty)
                return Description;
            return Name;
        }
    }
}