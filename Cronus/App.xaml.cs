using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Cronus.Services;
using Cronus.Stores;
using Cronus.Utilities;
using Cronus.ViewModels;
using Cronus.Views;

namespace Cronus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Used to manage the current <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;



        public App()
        {
            _bookStore = StoreFactory.CreateBookStore();
            _navigationStore = StoreFactory.CreateNavigationStore();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            // If the book store's current book is void state, navigate to
            // the Start Screen, otherwise navigate to the Book Details view.

            INavigate startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen",
                    _bookStore,
                    _navigationStore);
            startScreenNavigationService.Navigate();

            MainViewModel mainViewModel = new(_navigationStore);
            MainWindow = new MainView()
            {
                DataContext = mainViewModel
            };
            MainWindow.Show();

            // Catches the TextBox control's GotFocus event.
            EventManager.RegisterClassHandler(typeof(TextBox),
                TextBox.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            base.OnStartup(e);
        }



        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
        }
    }

}
