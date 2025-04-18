using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Services;
using Cronus.Stores;

namespace Cronus.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Used by the application manage the current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used by the application to determine the app's navigation
        /// state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// Returns the navigation store's
        /// <see cref="NavigationStore.CurrentMainContentViewModel"/>
        /// </summary>
        public ViewModelBase? CurrentContentViewModel =>
            _navigationStore.CurrentMainContentViewModel;



        public MainViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentMainContentViewModelChanged +=
                OnCurrentContentViewModelChanged;
        }



        public bool OnWindowClosing(object? sender, CancelEventArgs e)
        {
            // Save the app settings.
            SettingsService.SetAppSettings(_bookStore);

            // The book services CloseCurrentBook method returns
            // true if the user wishes to continue closing the
            // window, false otherwise.
            return BookService.CloseCurrentBook(_bookStore);
        }


        /// <summary>
        /// Handles the <see cref="ViewModelBase.PropertyChanged"/>
        /// event.
        /// </summary>
        private void OnCurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentContentViewModel)); 
        }
    }
}
