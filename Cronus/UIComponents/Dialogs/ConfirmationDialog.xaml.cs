using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ConfirmationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Used to determine the required text to be entered by the
        /// user.
        /// </summary>
        private string _requiredUserInputText;



        // Backing Fields
        private string confirmationText = string.Empty;


        /// <summary>
        /// Text for the Confirmation text box label.
        /// </summary>
        public string ConfirmationControlLabelText
        {
            set => ConfirmationControlLabel.Content = value;
        }

        /// <summary>
        /// Text for the Confirmation Message text block.
        /// </summary>
        public string ConfirmationMessageText
        {
            set => ConfirmationMessageTextBlock.Text = value;
        }

        /// <summary>
        /// Text for the Confirmation text box.
        /// </summary>
        public string ConfirmationText
        {
            get => confirmationText;
            set
            {
                confirmationText = value;
                OnPropertyChanged(nameof(ConfirmationText));
                OnPropertyChanged(nameof(IsConfirmationValid));
            }
        }

        /// <summary>
        /// Text for the dialog Accept button.
        /// </summary>
        public string DialogAcceptButtonText
        {
            set => DialogAcceptButton.Content = value;
        }

        /// <summary>
        /// True if <seealso cref="ConfirmationText"/>
        /// is equal to <seealso cref="_requiredUserInputText"/>.
        /// </summary>
        public bool IsConfirmationValid
        {
            get => ConfirmationText == _requiredUserInputText;
        }

        /// <summary>
        /// Text for the Title label.
        /// </summary>
        public string TitleText
        {
            set => TitleLabel.Content = value;
        }


        /// <summary>
        /// Raised when a property is set.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ConfirmationDialog"/> class.
        /// </summary>
        /// <param name="requiredUserInputText">User input text
        /// required to enable the dialog Accept button</param>
        public ConfirmationDialog(string requiredUserInputText)
        {
            _requiredUserInputText = requiredUserInputText;
            DataContext = this;

            InitializeComponent();
        }


        /// <summary>
        /// Sets the <see cref="Window.DialogResult"/> property to
        /// true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Sets the <see cref="Window.DialogResult"/> property to
        /// false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Handles the setting of a property.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
