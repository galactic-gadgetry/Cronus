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

        public string Name { get; set; } = string.Empty;

        public TimeSpan Duration { get; set; }

        public string DurationString => Duration.ToString("hh':'mm");
    }
}
