using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Stores;

namespace Cronus.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;



        public ViewModelBase? CurrentContentViewModel =>
            _navigationStore.CurrentMainContentViewModel;



        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentMainContentViewModelChanged +=
                OnCurrentContentViewModelChanged;
        }



        private void OnCurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentContentViewModel)); 
        }
    }
}
