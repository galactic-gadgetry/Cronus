using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cronus.Models
{
    public class TimeEntry
    {

        public Project? AssignedProject { get; set; } = null;


        public DateTime EndTime { get; set; }


        public Duration Duration => EndTime - StartTime;


        public DateTime StartTime { get; set; }


        public string TimeString => $"{StartTime.ToString("hh:mm tt")} - {EndTime.ToString("hh:mm tt")}";


        public string Title { get; set; } = string.Empty;
    }
}
