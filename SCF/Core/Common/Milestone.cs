using System;
using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core.Common
{
    [Table("Milestone", Schema = "Core")]
    public class Milestone : Entity
    {
        //MilestoneType
        public string Name { get; set; }
        public DateTime Time { get; set; }

        public bool IsChanged { get; set; }

        public Milestone()
        {
            Name = string.Empty;
            Time = DateTime.Today;
            IsChanged = false;
        }
    }
}