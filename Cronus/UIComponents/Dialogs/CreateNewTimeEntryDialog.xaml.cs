using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cronus.Models;
using Cronus.Services;

namespace Cronus.UIComponents.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewTimeEntryDialog.xaml
    /// </summary>
    public partial class CreateNewTimeEntryDialog : Window
    {
        /// <summary>
        /// Returns True if the dialog inputs are valid, false
        /// otherwise.
        /// </summary>
        public bool AreInputsValid => StartDateTime <= EndDateTime;

        /// <summary>
        /// Date for the new time entry.
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// Text for the Description text box.
        /// </summary>
        public string DescriptionText
        {
            get => DescriptionTextBox.Text;
            set => DescriptionTextBox.Text = value;
        }

        /// <summary>
        /// End date and time for the time entry.
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Array of strings for the Hour combo boxes.
        /// </summary>
        public string[] Hours { get; } = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };

        /// <summary>
        /// Array of strings for the Meridiem combo boxes.
        /// </summary>
        public string[] Meridiems { get; } = { "AM", "PM" };

        /// <summary>
        /// Array of strings for the Minutes combo boxes.
        /// </summary>
        public string[] Minutes { get; } = { "00", "15", "30", "45" };

        /// <summary>
        /// Collection of <see cref="Project"/> instances for the
        /// Project combo box.
        /// </summary>
        public ObservableCollection<Project> Projects { get; }

        /// <summary>
        /// The selected <see cref="Project"/> for the Projecct
        /// combo box.
        /// </summary>
        public Project SelectedProject { get; set; }

        /// <summary>
        /// Start date and time for the time entry.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Text for the Title text box.
        /// </summary>
        public string TitleText
        {
            get => TitleTextBox.Text;
            set => TitleTextBox.Text = value;
        }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="CreateNewTimeEntryDialog"/> class.
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="startDateTime"></param>
        public CreateNewTimeEntryDialog(ObservableCollection<Project> projects,
            DateTime startDateTime)
        {
            Projects = projects;
            SelectedProject = Projects[0];
            Date = startDateTime.Date;

            DataContext = this;

            InitializeComponent();

            InitializeTimeComboBoxes(startDateTime);
        }


        /// <summary>
        /// Handles the Back button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Handles the Create button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            SetTimes();
            if (!AreInputsValid)
            {
                string caption = "Invalid Timeframe";
                string message = "The end time cannot be set before " +
                    "the start time.";
                DialogService.PromptUserWithErrorMessageWithOKButtonDialog
                    (caption, message);

                return;
            }

            DialogResult = true;
        }

        /// <summary>
        /// Sets the time combo boxes according to the using the
        /// start date and time.
        /// </summary>
        /// <param name="startDateTime"></param>
        private void InitializeTimeComboBoxes(DateTime startDateTime)
        {
            // If the start time is 11:00 PM or earlier, set the
            // end time one hour later, otherwise set it at 12:00 PM.
            DateTime endDateTime;
            TimeSpan timeCheck = new(23, 0, 0);
            if (startDateTime.TimeOfDay > timeCheck)
            {
                endDateTime = startDateTime.Date;
                endDateTime = endDateTime.AddHours(24);
            }
            else
            {
                endDateTime = startDateTime.AddHours(1);
            }

            string startHour = startDateTime.ToString("hh");
            string startMinute = startDateTime.ToString("mm");
            string startMeridiem = startDateTime.ToString("tt", CultureInfo.InvariantCulture);
            string endHour = endDateTime.ToString("hh");
            string endMinute = endDateTime.ToString("mm");
            string endMeridiem = endDateTime.ToString("tt", CultureInfo.InvariantCulture);

            StartHourComboBox.SelectedItem = startHour;
            StartMinuteComboBox.SelectedItem = startMinute;
            StartMeridiemComboBox.SelectedItem = startMeridiem;
            EndHourComboBox.SelectedItem = endHour;
            EndMinuteComboBox.SelectedItem = endMinute;
            EndMeridiemComboBox.SelectedItem = endMeridiem;
        }

        /// <summary>
        /// Parses the time combo box inputs and converts them
        /// to date and times for the start and end date and times.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if the
        /// meridiem combo boxes' SelectedItem is null</exception>
        private void SetTimes()
        {
            int.TryParse(StartHourComboBox.SelectedItem.ToString(), out int startHour);
            int.TryParse(StartMinuteComboBox.SelectedItem.ToString(), out int startMinute);
            string? startMeridiem = StartMeridiemComboBox.SelectedItem.ToString();
            int.TryParse(EndHourComboBox.SelectedItem.ToString(), out int endHour);
            int.TryParse(EndMinuteComboBox.SelectedItem.ToString(), out int endMinute);
            string? endMeridiem = EndMeridiemComboBox.SelectedItem.ToString();

            if (startMeridiem == null)
            {
                throw new NullReferenceException(nameof(startMeridiem));
            }
            if (endMeridiem == null)
            {
                throw new NullReferenceException(nameof(endMeridiem));
            }

            if (startMeridiem.ToLower() == "pm")
            {
                startHour += 12;
            }
            if (endMeridiem.ToLower() == "pm")
            {
                endHour += 12;
            }

            TimeSpan startTimeSpan = new(startHour, startMinute, 0);
            TimeSpan endTimeSpan = new(endHour, endMinute, 0);
            StartDateTime = Date + startTimeSpan;
            EndDateTime = Date + endTimeSpan;
        }
    }
}
