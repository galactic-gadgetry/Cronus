using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Services;
using Cronus.Stores;
using Cronus.UIComponents.Cards;
using Cronus.UIComponents.Dialogs;

namespace Cronus.ViewModels
{
    public class DailyTimesheetViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the book store's current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;


        // Backing Fields
        private DateTime selectedDate;
        private TimeEntry? selectedTimeEntry;


        public Book CurrentBook => _bookStore.CurrentBook;



        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
            }
        }


        public TimeEntry? SelectedTimeEntry
        {
            get => selectedTimeEntry;
            set
            {
                selectedTimeEntry = value;
                // selectedItem.TestBorder.Background = Brushes.Yellow;
                OnPropertyChanged(nameof(SelectedTimeEntry));
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
            },
            new TimeEntry
            {
                Title = "Work",
                AssignedProject = new() { Name = "SpaceY" },
                StartTime = new(2025, 4, 25, 5, 0, 0),
                EndTime = new(2025, 4, 25, 6, 0, 0),
            },
        };



        public ICommand CreateEntryButtonClickedCommand { get; }



        public DailyTimesheetViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;
            selectedDate = DateTime.Today;
            SelectedTimeEntry = null;

            CreateEntryButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateEntryButtonClicked));
        }



        private void CreateNewTimeEntryRequested(
            CreateNewTimeEntryDialog dlg)
        {
            // Get the time inputs.
            int startHour = int.Parse(dlg.StartHourComboBox.Text);
            int startMinute = int.Parse(dlg.StartMinuteComboBox.Text);
            string startMeridiem = dlg.StartMeridiemComboBox.Text;
            int endHour = int.Parse(dlg.EndHourComboBox.Text);
            int endMinute = int.Parse(dlg.EndMinuteComboBox.Text);
            string endMeridiem = dlg.EndMeridiemComboBox.Text;

            // If either of the times is post meridiem, add 12 to
            // the integer.
            if (startMeridiem.ToLower() == "pm")
            {
                startHour += 12;
            }
            if (endMeridiem.ToLower() == "pm")
            {
                endHour += 12;
            }

            // Set the start and end date times.
            DateTime date = SelectedDate.Date;
            TimeSpan startTimeSpan = new(startHour, startMinute, 0);
            TimeSpan endTimeSpan = new(endHour, endMinute, 0);
            DateTime startDateTime = date + startTimeSpan;
            DateTime endDateTime = date + endTimeSpan;

            // Create a new TimeEntry from the dialog inputs.
            TimeEntryDTO dto = new()
            {
                AssignedProject = dlg.SelectedProject,
                EndTime = endDateTime,
                StartTime = startDateTime,
                Title = dlg.Title,
            };

            // If the time entry could not be created or added,
            // display an error message.
            throw new NotImplementedException();
            //(bool result, string? detail) =
            //    TimeEntryService.CreateNewTimeEntryInCurrentBook(_bookStore, dto);
            //if (result == false)
            //{
            //    throw new NotImplementedException();
            //}
        }


        private void OnCreateEntryButtonClicked(object? obj)
        {
            CreateNewTimeEntryDialog dlg =
                DialogService.PromptUserWithCreateNewTimeEntryDialog(CurrentBook);

            if (dlg.DialogResult == true)
            {
                CreateNewTimeEntryRequested(dlg);
            }
        }
    }
}
