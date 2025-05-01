using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class DailyTimesheetViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the book store's current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        // Backing Fields
        private DateTime selectedDate;
        private TimeEntry? selectedTimeEntry;

        /// <summary>
        /// Returns the book store's current <see cref="Book"/>.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;


        /// <summary>
        /// The currently selected date of the view.
        /// </summary>
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnSelectedDateChanged();
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        /// <summary>
        /// The currently selected <see cref="TimeEntry"/> in the
        /// Time Entries list.
        /// </summary>
        public TimeEntry? SelectedTimeEntry
        {
            get => selectedTimeEntry;
            set
            {
                selectedTimeEntry = value;
                OnPropertyChanged(nameof(SelectedTimeEntry));
            }
        }

        /// <summary>
        /// Returns the <see cref="TimeEntry"/> instances of the
        /// current book's <see cref="Book.TimeEntries"/> collection
        /// that occur on the <seealso cref="SelectedDate"/>.
        /// </summary>
        public ObservableCollection<TimeEntry> TimeEntries =>
            new(CurrentBook.TimeEntries
            .Where(t => t.Date == DateOnly.FromDateTime(SelectedDate)));


        /// <summary>
        /// Executed when the Create Entry button is clicked.
        /// </summary>
        public ICommand CreateEntryButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Next Day button is clicked.
        /// </summary>
        public ICommand NextDayButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Previous Day button is clicked.
        /// </summary>
        public ICommand PreviousDayButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the side content's Close button is clicked.
        /// </summary>
        public ICommand SideContentCloseButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the side content's Edit Details button
        /// is clicked.
        /// </summary>
        public ICommand SideContentEditDetailsButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Time Entry Card's Delete button is
        /// clicked.
        /// </summary>
        public ICommand TimeEntryCardDeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Time Entry Card's Edit button is
        /// clicked.
        /// </summary>
        public ICommand TimeEntryCardEditButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Today button is clicked.
        /// </summary>
        public ICommand TodayButtonClickedCommand { get; }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="DailyTimesheetViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public DailyTimesheetViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;
            selectedDate = DateTime.Today;
            SelectedTimeEntry = null;

            CreateEntryButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateEntryButtonClicked));
            NextDayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNextDayButtonClicked));
            PreviousDayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnPreviousDayButtonClicked));
            SideContentCloseButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSideContentCloseButtonClicked));
            SideContentEditDetailsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSideContentEditDetailsButtonClicked));
            TimeEntryCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnTimeEntryCardDeleteButtonClicked));
            TimeEntryCardEditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnTimeEntryCardEditButtonClicked));
            TodayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnTodayButtonClicked));

            CurrentBook.TimeEntries.CollectionChanged += OnTimeEntriesChanged;
        }


        /// <summary>
        /// Creates a new <see cref="TimeEntry"/> instance and adds
        /// it to the current book's <see cref="Book.TimeEntries"/>
        /// collection.
        /// </summary>
        /// <param name="dlg"></param>
        private void CreateNewTimeEntryRequested(
            CreateNewTimeEntryDialog dlg)
        {
            // Get start and end times.
            (DateTime startDateTime, DateTime endDateTime) = ParseDialogTimes(dlg);

            // Create a new TimeEntry from the dialog inputs.
            TimeEntryDTO dto = new()
            {
                AssignedProject = dlg.SelectedProject,
                Date = DateOnly.FromDateTime(SelectedDate),
                Description = dlg.DescriptionText,
                EndTime = endDateTime,
                StartTime = startDateTime,
                Title = dlg.TitleText,
            };

            // If the time entry could not be created or added,
            // display an error message.
            (bool result, string? detail) =
                TimeEntryService.CreateNewTimeEntryInCurrentBook(_bookStore, dto);
            if (result == false)
            {
                string caption = "Unable to Create New Entry";
                string message = "The selected timeframe overlaps " +
                    "the timeframe of another entry (entry " +
                    $"'{detail}').";
                DialogService.PromptUserWithErrorMessageWithOKButtonDialog(
                    caption, message);
            }
        }

        /// <summary>
        /// Handles the delete time entry request.
        /// </summary>
        /// <param name="timeEntry">TimeEntry instance to be
        /// deleted</param>
        private void DeleteTimeEntryRequested(TimeEntry timeEntry)
        {
            BookService.DeleteTimeEntryFromCurrentBook(_bookStore, timeEntry);

            // Update the info bar.
            OnInfoUpdated($"Time entry '{timeEntry.Title}' deleted");
        }

        /// <summary>
        /// Handles the edit time entry details request.
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="timeEntry"></param>
        private void EditTimeEntryRequested(EditTimeEntryDialog dlg,
            TimeEntry timeEntry)
        {
            // Get start and end times.
            (DateTime startDateTime, DateTime endDateTime) = ParseDialogTimes(dlg);
            
            TimeEntryDTO dto = new()
            {
                AssignedProject = (Project)dlg.ProjectComboBox.SelectedItem,
                Date = DateOnly.FromDateTime(startDateTime),
                Description = dlg.DescriptionText,
                EndTime= endDateTime,
                StartTime = startDateTime,
                Title = dlg.TitleText,
            };

            (bool result, string? detail) =
                TimeEntryService.EditTimeEntryDetails(_bookStore, timeEntry, dto);
            if (result)
            {
                // Save the book and called the OnPropertyChanged
                // method on the TimeEntries collection.
                BookService.SaveCurrentBookToJson(_bookStore);
                OnPropertyChanged(nameof(SelectedTimeEntry));
                OnPropertyChanged(nameof(TimeEntries));
            }
            else
            {
                string caption = "Unable to Edit Time Entry";
                string message = "The selected timeframe overlaps " +
                    "the timeframe of another entry (entry " +
                    $"'{detail}').";
                DialogService.PromptUserWithErrorMessageWithOKButtonDialog(
                    caption, message);
            }
        }

        /// <summary>
        /// Handles the Create Entry button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCreateEntryButtonClicked(object? obj)
        {
            CreateNewTimeEntryDialog dlg =
                DialogService.PromptUserWithCreateNewTimeEntryDialog(CurrentBook);

            if (dlg.DialogResult == true)
            {
                CreateNewTimeEntryRequested(dlg);
            }
        }

        /// <summary>
        /// Handles the Next Day button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnNextDayButtonClicked(object? obj)
        {
            SelectedDate = SelectedDate.AddDays(1);
        }

        /// <summary>
        /// Handles the Previous Day button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnPreviousDayButtonClicked(object? obj)
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        /// <summary>
        /// Handles the side content's Close button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnSideContentCloseButtonClicked(object? obj)
        {
            SelectedTimeEntry = null;
        }

        /// <summary>
        /// Handles the side content's Edit Details button click
        /// event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void OnSideContentEditDetailsButtonClicked(object? obj)
        {
            if (SelectedTimeEntry == null)
            {
                throw new NullReferenceException("SelectedTimeEntry " +
                    "must not be null");
            }

            EditTimeEntryDialog dlg = DialogService.PromptUserWithEditTimeEntryDialog(
                CurrentBook, SelectedTimeEntry);
            if (dlg.DialogResult == true)
            {
                EditTimeEntryRequested(dlg, SelectedTimeEntry);
            }
        }

        /// <summary>
        /// Handles the <seealso cref="SelectedDate"/> property being
        /// set.
        /// </summary>
        private void OnSelectedDateChanged()
        {
            OnPropertyChanged(nameof(TimeEntries));
        }

        /// <summary>
        /// Handles the Time Entry Card's Delete button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if
        /// the caller is null</exception>
        private void OnTimeEntryCardDeleteButtonClicked(object? obj)
        {
            TimeEntry? selectedTimeEntry = obj as TimeEntry;
            if (selectedTimeEntry == null)
            {
                throw new ArgumentOutOfRangeException("The caller " +
                    "must be a TimeEntry object");
            }

            if (DialogService.PromptUserWithSimpleDeleteConfirmationDialog(
                selectedTimeEntry) == true)
            {
                DeleteTimeEntryRequested(selectedTimeEntry);
            }
        }

        /// <summary>
        /// Handles the Time Entry Card's Edit button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if
        /// the caller is null</exception>
        private void OnTimeEntryCardEditButtonClicked(object? obj)
        {
            TimeEntry? selectedTimeEntry = obj as TimeEntry;
            if (selectedTimeEntry == null)
            {
                throw new ArgumentOutOfRangeException("The caller " +
                    "must be a TimeEntry object");
            }

            EditTimeEntryDialog dlg = DialogService.PromptUserWithEditTimeEntryDialog(
                CurrentBook, selectedTimeEntry);
            if (dlg.DialogResult == true)
            {
                EditTimeEntryRequested(dlg, selectedTimeEntry);
            }
        }

        /// <summary>
        /// Handles the <see cref="Book.TimeEntries"/> collection
        /// changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimeEntriesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TimeEntries));
        }

        /// <summary>
        /// Handles the Today button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnTodayButtonClicked(object? obj)
        {
            SelectedDate = DateTime.Today;
        }

        /// <summary>
        /// Parses the dialog's time inputs.
        /// </summary>
        /// <param name="dlg"></param>
        /// <returns>Start and end DateTime instances</returns>
        private (DateTime, DateTime) ParseDialogTimes(CreateNewTimeEntryDialog dlg)
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

            return (startDateTime, endDateTime);
        }

        /// <summary>
        /// Parses the dialog's time inputs.
        /// </summary>
        /// <param name="dlg"></param>
        /// <returns>Start and end DateTime instances</returns>
        private (DateTime, DateTime) ParseDialogTimes(EditTimeEntryDialog dlg)
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

            return (startDateTime, endDateTime);
        }
    }
}
