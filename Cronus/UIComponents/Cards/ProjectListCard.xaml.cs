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

namespace Cronus.UIComponents.Cards
{
    /// <summary>
    /// Interaction logic for ProjectListCard.xaml
    /// </summary>
    public partial class ProjectListCard : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty CodeTextProperty =
            DependencyProperty.Register(
                nameof(CodeText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DeleteButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommandParameter),
                typeof(object),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommand),
                typeof(ICommand),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty EditButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommandParameter),
                typeof(object),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty EditButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommand),
                typeof(ICommand),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NameHyperlinkClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(NameHyperlinkClickedCommandParameter),
                typeof(object),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NameHyperlinkClickedCommandProperty =
            DependencyProperty.Register(
                nameof(NameHyperlinkClickedCommand),
                typeof(ICommand),
                typeof(ProjectListCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NameTextProperty =
            DependencyProperty.Register(
                nameof(NameText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register(
                nameof(StatusText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TimeEntriesCountTextProperty =
            DependencyProperty.Register(
                nameof(TimeEntriesCountText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TotalTimeTextProperty =
            DependencyProperty.Register(
                nameof(TotalTimeText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty WbsTextProperty =
            DependencyProperty.Register(
                nameof(WbsText),
                typeof(string),
                typeof(ProjectListCard),
                new PropertyMetadata(string.Empty));


        /// <summary>
        /// Text for the card's Code label.
        /// </summary>
        public string CodeText
        {
            get => (string)GetValue(CodeTextProperty);
            set => SetValue(CodeTextProperty, value);
        }

        /// <summary>
        /// Parameter passed when the Delete button is clicked.
        /// </summary>
        public object DeleteButtonClickedCommandParameter
        {
            get => (object)GetValue(DeleteButtonClickedCommandParameterProperty);
            set => SetValue(DeleteButtonClickedCommandParameterProperty, value);
        }

        /// <summary>
        /// Parameter passed when the Edit button is clicked.
        /// </summary>
        public object EditButtonClickedCommandParameter
        {
            get => (object)GetValue(EditButtonClickedCommandParameterProperty);
            set => SetValue(EditButtonClickedCommandParameterProperty, value);
        }

        /// <summary>
        /// Parameter passed when the Name hyperlink is clicked.
        /// </summary>
        public object NameHyperlinkClickedCommandParameter
        {
            get => (object)GetValue(NameHyperlinkClickedCommandParameterProperty);
            set => SetValue(NameHyperlinkClickedCommandParameterProperty, value);
        }

        /// <summary>
        /// Text for the card's Name label.
        /// </summary>
        public string NameText
        {
            get => (string)GetValue(NameTextProperty);
            set => SetValue(NameTextProperty, value);
        }

        /// <summary>
        /// Text for the card's Status label.
        /// </summary>
        public string StatusText
        {
            get => (string)GetValue(StatusTextProperty);
            set => SetValue(StatusTextProperty, value);
        }

        /// <summary>
        /// Text for the Time Entries label.
        /// </summary>
        public string TimeEntriesCountText
        {
            get => (string)GetValue(TimeEntriesCountTextProperty);
            set => SetValue(TimeEntriesCountTextProperty, value);
        }

        /// <summary>
        /// Text for the Total Time label.
        /// </summary>
        public string TotalTimeText
        {
            get => (string)GetValue(TotalTimeTextProperty);
            set => SetValue(TotalTimeTextProperty, value);
        }

        /// <summary>
        /// Text for the card's WBS label.
        /// </summary>
        public string WbsText
        {
            get => (String)GetValue(WbsTextProperty);
            set => SetValue(WbsTextProperty, value);
        }



        /// <summary>
        /// Executed when the Delete button is clicked.
        /// </summary>
        public ICommand DeleteButtonClickedCommand
        {
            get => (ICommand)GetValue(DeleteButtonClickedCommandProperty);
            set => SetValue(DeleteButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Edit button is clicked.
        /// </summary>
        public ICommand EditButtonClickedCommand
        {
            get => (ICommand)GetValue(EditButtonClickedCommandProperty);
            set => SetValue(EditButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Name hyperlink is clicked.
        /// </summary>
        public ICommand NameHyperlinkClickedCommand
        {
            get => (ICommand)GetValue(NameHyperlinkClickedCommandProperty);
            set => SetValue(NameHyperlinkClickedCommandProperty, value);
        }



        public ProjectListCard()
        {
            InitializeComponent();
        }
    }
}
