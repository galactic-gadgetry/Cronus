using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for CreateNewTimeEntryDialog.xaml
    /// </summary>
    public partial class CreateNewTimeEntryDialog : Window
    {
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
        /// The selected <see cref="Project"/> for the Projecct
        /// combo box.
        /// </summary>
        public Project SelectedProject { get; set; }

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
        public CreateNewTimeEntryDialog(ObservableCollection<Project> projects)
        {
            Projects = projects;
            SelectedProject = Projects[0];

            DataContext = this;

            InitializeComponent();
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
            DialogResult = true;
        }
    }
}
