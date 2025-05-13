using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cronus.Models
{
    public class ProjectStatistic
    {

        public string Code { get; set; } = string.Empty;

        public TimeSpan Duration { get; set; }

        public string DurationString
        {
            get
            {
                return (string.Format("{0:00}:{1:00}", (int)Duration.TotalHours, Duration.Minutes));
            }
        }

        public Guid ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public ProjectStatus Status { get; set; }

        public string StatusText => Status.ToString();

        public int TimeEntriesCount { get; set; }

        public string TimeEntriesCountString => TimeEntriesCount.ToString();

        public string Wbs { get; set; } = string.Empty;


        public ProjectStatistic() { }


        public ProjectStatistic(Project project)
        {
            Code = project.Code;
            ID = project.ID;
            Name = project.Name;
            Status = project.Status;
            Wbs = project.Wbs;
        }
    }
}
