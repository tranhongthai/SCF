using System;
using Peyton.Core.Repository;

namespace Peyton.Core.Calendar
{
    public class Appointment : Entity
    {
        public Appointment()
        {
            Subject = string.Empty;
            Note = string.Empty;
        }

        public string Subject { get; set; }
        public DateTime? StartTime { get; set; }
        public string Note { get; set; }

        public virtual AppointmentType Type { get; set; }
    }
}