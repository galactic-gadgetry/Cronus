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
    /// Interaction logic for SmallSavedBookCard.xaml
    /// </summary>
    public partial class SmallSavedBookCard : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty BookNameTextProperty =
            DependencyProperty.Register(
                nameof(BookNameText),
                typeof(string),
                typeof(SmallSavedBookCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BookStatusTextProperty =
            DependencyProperty.Register(
                nameof(BookStatusText),
                typeof(string),
                typeof(SmallSavedBookCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty MoreButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(MoreButtonClickedCommandParameter),
                typeof(object),
                typeof(SmallSavedBookCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty MoreButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(MoreButtonClickedCommand),
                typeof(ICommand),
                typeof(SmallSavedBookCard),
                new PropertyMetadata(null));


        /// <summary>
        /// Text for the Book Name label.
        /// </summary>
        public string BookNameText
        {
            get => (string)GetValue(BookNameTextProperty);
            set => SetValue(BookNameTextProperty, value);
        }

        /// <summary>
        /// Text for the Book Status label.
        /// </summary>
        public string BookStatusText
        {
            get => (string)GetValue(BookStatusTextProperty);
            set => SetValue(BookStatusTextProperty, value);
        }


        /// <summary>
        /// Command used for the More button clicked event.
        /// </summary>
        public ICommand MoreButtonClickedCommand
        {
            get => (ICommand)GetValue(MoreButtonClickedCommandProperty);
            set => SetValue(MoreButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Object used for the More button command parameter.
        /// </summary>
        public object MoreButtonClickedCommandParameter
        {
            get => (object)GetValue(MoreButtonClickedCommandParameterProperty);
            set => SetValue(MoreButtonClickedCommandParameterProperty, value);
        }



        public SmallSavedBookCard()
        {
            InitializeComponent();
        }
    }
}
