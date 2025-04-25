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
    /// Interaction logic for DailyTimesheetEntryCard.xaml
    /// </summary>
    public partial class DailyTimesheetEntryCard : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty DeleteButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommand),
                typeof(ICommand),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(DeleteButtonClickedCommandParameter),
                typeof(object),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DurationTextProperty =
            DependencyProperty.Register(
                nameof(DurationText),
                typeof(string),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty EditButtonClickedCommandProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommand),
                typeof(ICommand),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty EditButtonClickedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(EditButtonClickedCommandParameter),
                typeof(object),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ProjectNameTextProperty =
            DependencyProperty.Register(
                nameof(ProjectNameText),
                typeof(string),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TimeTextProperty =
            DependencyProperty.Register(
                nameof(TimeText),
                typeof(string),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register(
                nameof(TitleText),
                typeof(string),
                typeof(DailyTimesheetEntryCard),
                new PropertyMetadata(string.Empty));



        public object DeleteButtonClickedCommandParameter
        {
            get => (object)GetValue(DeleteButtonClickedCommandParameterProperty);
            set => SetValue(DeleteButtonClickedCommandParameterProperty, value);
        }


        public string DurationText
        {
            get => (string)GetValue(DurationTextProperty);
            set => SetValue(DurationTextProperty, value);
        }


        public object EditButtonClickedCommandParameter
        {
            get => (object)GetValue(EditButtonClickedCommandParameterProperty);
            set => SetValue(EditButtonClickedCommandParameterProperty, value);
        }


        public string ProjectNameText
        {
            get => (string)(GetValue(ProjectNameTextProperty));
            set => SetValue(ProjectNameTextProperty, value);
        }


        public string TimeText
        {
            get => ((string)(GetValue(TimeTextProperty)));
            set => SetValue(TimeTextProperty, value);
        }


        public string TitleText
        {
            get => (String)(GetValue(TitleTextProperty));
            set => SetValue(TitleTextProperty, value);
        }



        public ICommand EditButtonClickedCommand
        {
            get => (ICommand)GetValue(EditButtonClickedCommandProperty);
            set => SetValue(EditButtonClickedCommandProperty, value);
        }


        public ICommand DeleteButtonClickedCommand
        {
            get => (ICommand)(GetValue(DeleteButtonClickedCommandProperty));
            set => SetValue(DeleteButtonClickedCommandProperty, value);
        }


                
        public DailyTimesheetEntryCard()
        {
            InitializeComponent();
        }
    }
}
