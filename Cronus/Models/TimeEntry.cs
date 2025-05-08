using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cronus.Models
{
    public class TimeEntry : IConfirmDeletion, INotifyPropertyChanged
    {
        // Backing Fields
        private Project? assignedProject = null;
        private DateOnly date;
        private DateTime endTime;
        private string description = string.Empty;
        private DateTime startTime;
        public string title = string.Empty;



        public Project? AssignedProject
        {
            get => assignedProject;
            set
            {
                assignedProject = value;
                OnPropertyChanged(nameof(AssignedProject));
            }
        }


        public DateOnly Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        public DateTime EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
                OnPropertyChanged(nameof(EndTime));
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(TimeString));
            }
        }


        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public TimeSpan Duration => EndTime - StartTime;


        public DateTime StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(TimeString));
            }
        }


        public string TimeString => $"{StartTime.ToString("hh:mm tt")} - {EndTime.ToString("hh:mm tt")}";


        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;



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


        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
