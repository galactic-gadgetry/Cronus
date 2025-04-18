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

namespace Cronus.UIComponents.Buttons
{
    /// <summary>
    /// Interaction logic for NavigationBarButton.xaml
    /// </summary>
    public partial class NavigationBarButton : UserControl
    {
        // Get the default button's resource dictionary.
        private static readonly ResourceDictionary res =
            (ResourceDictionary)Application.LoadComponent(
                new Uri("/LadleUI;component/Controls/Button.xaml", UriKind.Relative));
        private static readonly Thickness buttonPadding = (Thickness)res["ButtonPadding"];

        // Dependency Properties
        public static readonly DependencyProperty ButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(ButtonClickedCommand),
                typeof(ICommand),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourceProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSource),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourceDisabledProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSourceDisabled),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourceHighlightedProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSourceHighlighted),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourceMouseOverProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSourceMouseOver),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonImageSourcePressedProperty =
            DependencyProperty.Register(
                nameof(ButtonImageSourcePressed),
                typeof(ImageSource),
                typeof(NavigationBarButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ButtonPaddingProperty =
            DependencyProperty.Register(
                nameof(ButtonPadding),
                typeof(Thickness),
                typeof(NavigationBarButton),
                new PropertyMetadata(buttonPadding));

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(string),
                typeof(NavigationBarButton),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(
                nameof(IsHighlighted),
                typeof(bool),
                typeof(NavigationBarButton),
                new PropertyMetadata(false));


        /// <summary>
        /// Source for button's image.
        /// </summary>
        public ImageSource ButtonImageSource
        {
            get => (ImageSource)GetValue(ButtonImageSourceProperty);
            set => SetValue(ButtonImageSourceProperty, value);
        }

        /// <summary>
        /// Source for the button's image source when disabled.
        /// </summary>
        public ImageSource ButtonImageSourceDisabled
        {
            get => (ImageSource)GetValue(ButtonImageSourceDisabledProperty);
            set => SetValue(ButtonImageSourceDisabledProperty, value);
        }

        /// <summary>
        /// Source for the button's image when highlighted.
        /// </summary>
        public ImageSource ButtonImageSourceHighlighted
        {
            get => (ImageSource)GetValue(ButtonImageSourceHighlightedProperty);
            set => SetValue(ButtonImageSourceHighlightedProperty, value);
        }

        /// <summary>
        /// Source for the button's image source on mouse over.
        /// </summary>
        public ImageSource ButtonImageSourceMouseOver
        {
            get => (ImageSource)GetValue(ButtonImageSourceMouseOverProperty);
            set => SetValue(ButtonImageSourceMouseOverProperty, value);
        }

        /// <summary>
        /// Source for the button's image source when pressed.
        /// </summary>
        public ImageSource ButtonImageSourcePressed
        {
            get => (ImageSource)GetValue(ButtonImageSourcePressedProperty);
            set => SetValue(ButtonImageSourcePressedProperty, value);
        }

        /// <summary>
        /// Padding for the button.
        /// </summary>
        public Thickness ButtonPadding
        {
            get => (Thickness)GetValue(ButtonPaddingProperty);
            set => SetValue(ButtonPaddingProperty, value);
        }

        /// <summary>
        /// Text for button text block.
        /// </summary>
        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        /// <summary>
        /// True if the button is highlighted, false otherwise.
        /// </summary>
        public bool IsHighlighted
        {
            get => (bool)GetValue(IsHighlightedProperty);
            set => SetValue(IsHighlightedProperty, value);
        }


        /// <summary>
        /// Executed when the button is clicked.
        /// </summary>
        public ICommand ButtonClickedCommand
        {
            get => (ICommand)GetValue(ButtonClickedCommandProperty);
            set => SetValue(ButtonClickedCommandProperty, value);
        }



        public NavigationBarButton()
        {
            InitializeComponent();
        }
    }
}
