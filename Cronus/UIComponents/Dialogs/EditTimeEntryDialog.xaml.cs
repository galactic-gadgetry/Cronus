using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

namespace Cronus.UIComponents.Dialogs
{
    /// <summary>
    /// Interaction logic for EditTimeEntryDialog.xaml
    /// </summary>
    public partial class EditTimeEntryDialog : Window
    {
        /// <summary>
        /// Text for the Description text box.
        /// </summary>
        public string DescriptionText
        {
            get => DescriptionTextBox.Text;
            set => DescriptionTextBox.Text = value;
        }

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
        /// Text for the Title text box.
        /// </summary>
        public string TitleText
        {
            get => TitleTextBox.Text;
            set => TitleTextBox.Text = value;
        }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="EditTimeEntryDialog"/> class.
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="timeEntry"></param>
        public EditTimeEntryDialog(ObservableCollection<Project> projects,
            TimeEntry timeEntry)
        {
            Projects = projects;

            DataContext = this;

            InitializeComponent();

            SetInputs(timeEntry);
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
        /// Handles the Save button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Sets the input fields of the dialog window.
        /// </summary>
        /// <param name="timeEntry"></param>
        private void SetInputs(TimeEntry timeEntry)
        {
            // Parse time into strings.
            string startHour = timeEntry.StartTime.Hour.ToString();
            string startMinute = timeEntry.StartTime.Minute.ToString();
            string startMeridiem = timeEntry.StartTime.ToString("tt", CultureInfo.InvariantCulture);
            string endHour = timeEntry.EndTime.Hour.ToString();
            string endMinute = timeEntry.EndTime.Minute.ToString();
            string endMeridiem = timeEntry.EndTime.ToString("tt", CultureInfo.InvariantCulture);

            // Set time combo boxes.
            StartHourComboBox.SelectedItem = startHour;
            StartMinuteComboBox.SelectedItem = startMinute;
            StartMeridiemComboBox.SelectedItem = startMeridiem;
            EndHourComboBox.SelectedItem = endHour;
            EndMinuteComboBox.SelectedItem = endMinute;
            EndMeridiemComboBox.SelectedItem = endMeridiem;

            DescriptionText = timeEntry.Description;
            if (timeEntry.AssignedProject != null)
            {
                ProjectComboBox.SelectedItem = Projects.Single(p => p.ID == timeEntry.AssignedProject.ID);
            }
            else
            {
                ProjectComboBox.SelectedItem = Projects[0];
            }
                TitleText = timeEntry.Title;
        }
    }
}
