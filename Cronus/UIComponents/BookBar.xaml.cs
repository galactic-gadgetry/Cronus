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

namespace Cronus.UIComponents
{
    /// <summary>
    /// Interaction logic for BookBar.xaml
    /// </summary>
    public partial class BookBar : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty BookNameButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(BookNameButtonClickedCommand),
                typeof(ICommand),
                typeof(BookBar),
                new PropertyMetadata(null));

        public static readonly DependencyProperty BookNameButtonIsHitTestVisibleProperty =
            DependencyProperty.Register(
                nameof(BookNameButtonIsHitTestVisible),
                typeof(bool),
                typeof(BookBar),
                new PropertyMetadata(true));

        public static readonly DependencyProperty BookNameTextProperty =
            DependencyProperty.Register(
                nameof(BookNameText),
                typeof(string),
                typeof(BookBar),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BookStatusTextProperty =
            DependencyProperty.Register(
                nameof(BookStatusText),
                typeof(string),
                typeof(BookBar),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsSaveBookButtonEnabledProperty =
            DependencyProperty.Register(
                nameof(IsSaveBookButtonEnabled),
                typeof(bool),
                typeof(BookBar),
                new PropertyMetadata(false));

        public static readonly DependencyProperty SaveBookButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(SaveBookButtonClickedCommand),
                typeof(ICommand),
                typeof(BookBar),
                new PropertyMetadata(null));

        public static readonly DependencyProperty SettingsButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(SettingsButtonClickedCommand),
                typeof(ICommand),
                typeof(BookBar),
                new PropertyMetadata(null));


        /// <summary>
        /// True if the button is visible to a hit test, false
        /// otherwise.
        /// </summary>
        public bool BookNameButtonIsHitTestVisible
        {
            get => (bool)GetValue(BookNameButtonIsHitTestVisibleProperty);
            set => SetValue(BookNameButtonIsHitTestVisibleProperty, value);
        }

        /// <summary>
        /// Text for the Book Name button name label.
        /// </summary>
        public string BookNameText
        {
            get => (string)GetValue(BookNameTextProperty);
            set => SetValue(BookNameTextProperty, value);
        }

        /// <summary>
        /// Text for the Book Name button status label.
        /// </summary>
        public string BookStatusText
        {
            get => (string)GetValue(BookStatusTextProperty);
            set => SetValue(BookStatusTextProperty, value);
        }

        /// <summary>
        /// True if the Save Book button is enabled, false otherwise.
        /// </summary>
        public bool IsSaveBookButtonEnabled
        {
            get => (bool)GetValue(IsSaveBookButtonEnabledProperty);
            set => SetValue(IsSaveBookButtonEnabledProperty, value);
        }


        /// <summary>
        /// Executed when the Book Name button is clicked.
        /// </summary>
        public ICommand BookNameButtonClickedCommand
        {
            get => (ICommand)GetValue(BookNameButtonClickedCommandProperty);
            set => SetValue(BookNameButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Save Book button is clicked.
        /// </summary>
        public ICommand SaveBookButtonClickedCommand
        {
            get => (ICommand)GetValue(SaveBookButtonClickedCommandProperty);
            set => SetValue(SaveBookButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Settings button is clicked.
        /// </summary>
        public ICommand SettingsButtonClickedCommand
        {
            get => (ICommand)GetValue(SettingsButtonClickedCommandProperty);
            set => SetValue(SettingsButtonClickedCommandProperty, value);
        }



        public BookBar()
        {
            InitializeComponent();
        }
    }
}
