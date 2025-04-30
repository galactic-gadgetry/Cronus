using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cronus.Models.DTOs
{
    public class TimeEntryDTO
    {

        public Project? AssignedProject { get; set; } = null;


        public DateTime EndTime { get; set; }


        public DateTime StartTime { get; set; }


        public string Title { get; set; } = string.Empty;
    }
}
