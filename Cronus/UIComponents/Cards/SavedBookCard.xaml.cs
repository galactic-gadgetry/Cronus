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
    /// Interaction logic for SavedBookCard.xaml
    /// </summary>
    public partial class SavedBookCard : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty CreatedDateStringTextProperty =
            DependencyProperty.Register(
                nameof(CreatedDateStringText),
                typeof(string),
                typeof(SavedBookCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty DeleteButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommandParameter),
                typeof(object),
                typeof(SavedBookCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommand),
                typeof(ICommand),
                typeof(SavedBookCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty LoadButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(LoadButtonClickedCommandParameter),
                typeof(object),
                typeof(SavedBookCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty LoadButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(LoadButtonClickedCommand),
                typeof(ICommand),
                typeof(SavedBookCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NameTextProperty =
            DependencyProperty.Register(
                nameof(NameText),
                typeof(string),
                typeof(SavedBookCard),
                new PropertyMetadata(string.Empty));


        /// <summary>
        /// Text for the card's Created On label.
        /// </summary>
        public string CreatedDateStringText
        {
            get => (string)GetValue(CreatedDateStringTextProperty);
            set => SetValue(CreatedDateStringTextProperty, value);
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
        /// Parameter passed when the Load button is clicked.
        /// </summary>
        public object LoadButtonClickedCommandParameter
        {
            get => (object)GetValue(LoadButtonClickedCommandParameterProperty);
            set => SetValue(LoadButtonClickedCommandParameterProperty, value);
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
        /// Executed when the Delete button is clicked.
        /// </summary>
        public ICommand DeleteButtonClickedCommand
        {
            get => (ICommand)GetValue(DeleteButtonClickedCommandProperty);
            set => SetValue(DeleteButtonClickedCommandProperty, value);
        }

        /// <summary>
        /// Executed when the Load button is clicked.
        /// </summary>
        public ICommand LoadButtonClickedCommand
        {
            get => (ICommand)GetValue(LoadButtonClickedCommandProperty);
            set => SetValue(LoadButtonClickedCommandProperty, value);
        }



        public SavedBookCard()
        {
            InitializeComponent();
        }
    }
}
