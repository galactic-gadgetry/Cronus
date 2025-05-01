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
                OnSelectedTimeEntryChanged();
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
        /// Executed when the Today button is clicked.
        /// </summary>
        public ICommand TodayButtonClickedCommand { get; }



        public DailyTimesheetViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;
            selectedDate = DateTime.Today;
            SelectedTimeEntry = null;

            CreateEntryButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateEntryButtonClicked));
            TodayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnTodayButtonClicked));
            NextDayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNextDayButtonClicked));
            PreviousDayButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnPreviousDayButtonClicked));
            SideContentCloseButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSideContentCloseButtonClicked));

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
        /// Handles the <seealso cref="SelectedDate"/> property being
        /// set.
        /// </summary>
        private void OnSelectedDateChanged()
        {
            OnPropertyChanged(nameof(TimeEntries));
        }


        private void OnSelectedTimeEntryChanged()
        {
            return;
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
    }
}
