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
    /// Interaction logic for SimpleConfirmationDialog.xaml
    /// </summary>
    public partial class SimpleConfirmationDialog : Window
    {
        /// <summary>
        /// Text for the Confirmation Message text block.
        /// </summary>
        public string ConfirmationMessageText
        {
            set => ConfirmationMessageTextBlock.Text = value;
        }

        /// <summary>
        /// Text for the Accept button.
        /// </summary>
        public string DialogAcceptButtonText
        {
            set => DialogAcceptButton.Content = value;
        }

        /// <summary>
        /// Text for the Title label.
        /// </summary>
        public string TitleText
        {
            set => TitleLabel.Content = value;
        }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="SimpleConfirmationDialog"/> class.
        /// </summary>
        public SimpleConfirmationDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the Accept button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
