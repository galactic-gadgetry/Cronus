using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Cronus.Models;
using Cronus.UIComponents.Cards;

namespace Cronus.ViewModels
{
    public class DailyTimesheetViewModel : DefaultViewModelBase
    {

        private DailyTimesheetEntryCard selectedItem;

        public DailyTimesheetEntryCard SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                selectedItem.TestBorder.Background = Brushes.Yellow;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        public ObservableCollection<TimeEntry> TimeEntries { get; set; } = new()
        {
            new TimeEntry
            {
                Title = "Test Entry",
                AssignedProject = new() { Name = "Test Project" },
                StartTime = new(2025, 4, 25, 1, 0, 0),
                EndTime = new(2025, 4, 25, 5, 0, 0),
            }
        };
    }
}
