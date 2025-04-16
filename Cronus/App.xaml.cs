using System.Configuration;
using System.Data;
using System.Windows;
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

        private readonly BookStore _bookStore;


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

            base.OnStartup(e);
        }
    }

}
