using System;
using System.Collections.Generic;
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

namespace Cronus.UIComponents.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewLogBookDialog.xaml
    /// </summary>
    public partial class CreateNewLogBookDialog : Window
    {
        /// <summary>
        /// Text for the Name text box.
        /// </summary>
        public string NameText
        {
            get => NameTextBox.Text;
            set => NameTextBox.Text = value;
        }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="CreateNewLogBookDialog"/> class.
        /// </summary>
        public CreateNewLogBookDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Sets the <see cref="Window.DialogResult"/> property to
        /// false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Sets the <see cref="Window.DialogResult"/> property to
        /// true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
