using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cronus.Models
{
    public class TimeEntry : IConfirmDeletion
    {

        public Project? AssignedProject { get; set; } = null;


        public DateOnly Date { get; set; }


        public DateTime EndTime { get; set; }


        public string Description { get; set; } = string.Empty;


        public TimeSpan Duration => EndTime - StartTime;


        public DateTime StartTime { get; set; }


        public string TimeString => $"{StartTime.ToString("hh:mm tt")} - {EndTime.ToString("hh:mm tt")}";


        public string Title { get; set; } = string.Empty;



        public (bool, string) ContainsTimeframeConflict(TimeEntry timeEntry)
        {
            // Check that the time entry dates match.
            if (timeEntry.Date != Date)
            {
                throw new ArgumentOutOfRangeException(Date.ToString(), "Time " +
                    "entry dates do not match");
            }

            if (timeEntry.StartTime < EndTime && StartTime < timeEntry.EndTime)
            {
                return (true, Title);
            }
            else
            {
                return (false, Title);
            }
        }



        public string GetModelTypeString()
        {
            return "Time Entry";
        }
    }
}
