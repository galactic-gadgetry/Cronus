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
    /// Interaction logic for ErrorMessageWithOKButtonDialog.xaml
    /// </summary>
    public partial class ErrorMessageWithOKButtonDialog : Window
    {
        /// <summary>
        /// Text for the Message text block.
        /// </summary>
        public string MessageText
        {
            set => MessageTextBlock.Text = value;
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
        /// <seealso cref="ErrorMessageWithOKButtonDialog"/>
        /// class.
        /// </summary>
        public ErrorMessageWithOKButtonDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the OK button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
