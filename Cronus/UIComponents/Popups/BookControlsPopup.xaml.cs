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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cronus.UIComponents.Popups
{
    /// <summary>
    /// Interaction logic for BookControlsPopup.xaml
    /// </summary>
    public partial class BookControlsPopup : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty BookNameTextProperty =
            DependencyProperty.Register(
                nameof(BookNameText),
                typeof(string),
                typeof(BookControlsPopup),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BookStatusTextProperty =
            DependencyProperty.Register(
                nameof(BookStatusText),
                typeof(string),
                typeof(BookControlsPopup),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty CloseBookButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(CloseBookButtonClickedCommand),
                typeof(ICommand),
                typeof(BookControlsPopup),
                new PropertyMetadata(null));

        public static readonly DependencyProperty CreatedOnTextProperty =
            DependencyProperty.Register(
                nameof(CreatedOnText),
                typeof(string),
                typeof(BookControlsPopup),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty EditBookDetailsButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(EditBookDetailsButtonClickedCommand),
                typeof(ICommand),
                typeof(BookControlsPopup),
                new PropertyMetadata(null));

        public static readonly DependencyProperty SwitchBooksButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(SwitchBooksButtonClickedCommand),
                typeof(ICommand),
                typeof(BookControlsPopup),
                new PropertyMetadata(null));


        /// <summary>
        /// Text for the Book's name label.
        /// </summary>
        public string BookNameText
        {
            get => (string)GetValue(BookNameTextProperty);
            set => SetValue(BookNameTextProperty, value);
        }

        /// <summary>
        /// Text for the Book's status label.
        /// </summary>
        public string BookStatusText
        {
            get => (String)GetValue(BookStatusTextProperty);
            set => SetValue(BookStatusTextProperty, value);
        }

        /// <summary>
        /// Text for the Created On label.
        /// </summary>
        public string CreatedOnText
        {
            get => (string)GetValue(CreatedOnTextProperty);
            set => SetValue(CreatedOnTextProperty, value);
        }


        /// <summary>
        /// Executed when the Close Log Book button is clicked.
        /// </summary>
        public ICommand CloseBookButtonClickedCommand
        {
            get => (ICommand)GetValue(CloseBookButtonClickedCommandProperty);
            set => SetValue(CloseBookButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Edit Log Book button is clicked.
        /// </summary>
        public ICommand EditBookDetailsButtonClickedCommand
        {
            get => (ICommand)GetValue(EditBookDetailsButtonClickedCommandProperty);
            set => SetValue(EditBookDetailsButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Switch Log Books button is clicked.
        /// </summary>
        public ICommand SwitchBooksButtonClickedCommand
        {
            get => (ICommand)GetValue(SwitchBooksButtonClickedCommandProperty);
            set => SetValue(SwitchBooksButtonClickedCommandProperty, value);
        }




        public BookControlsPopup()
        {
            InitializeComponent();
        }
    }
}
