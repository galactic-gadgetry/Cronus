using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Services;
using Cronus.Stores;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {

        private readonly BookStore _bookStore;


        private readonly NavigationStore _navigationStore;


        private readonly INavigate _settingsNavBarNavigationSerivce;


        private readonly INavigate _sideContentNavigationService;


        // Backing Fields
        private string infoText = string.Empty;



        public ViewModelBase? CurrentContentViewModel =>
            _navigationStore.CurrentLayoutContentViewModel;
        public ViewModelBase? CurrentNavigationBarViewModel =>
            _navigationStore.CurrentNavigationBarViewModel;

        public ViewModelBase? CurrentSideContentViewModel =>
            _navigationStore.CurrentSideContentViewModel;


        public string InfoText
        {
            get => infoText;
            set
            {
                infoText = value;
                OnPropertyChanged(nameof(InfoText));
            }
        }



        public LayoutViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            _navigationStore.CurrentLayoutContentViewModelChanged +=
                OnCurrentLayoutContentViewModelChanged;
            _navigationStore.CurrentNavigationBarViewModelChanged +=
                OnCurrentNavigationBarViewModelChanged;
            _navigationStore.CurrentSideContentViewModelChanged +=
                OnCurrentSideContentViewModelChanged;

            _settingsNavBarNavigationSerivce =
                ServiceFactory.CreateNavigationService(
                    "settings nav bar",
                    _bookStore,
                    _navigationStore);
            _sideContentNavigationService =
                ServiceFactory.CreateNavigationService(
                    "x button side bar",
                    _bookStore,
                    _navigationStore);
        }



        private void NavigateDefaultView()
        {
            throw new NotImplementedException();
        }


        private void NavigateDefaultViewConstituents()
        {
            throw new NotImplementedException();
        }


        private void NavigateSettingsViewConstituents()
        {
            _settingsNavBarNavigationSerivce.Navigate();
            _sideContentNavigationService.Navigate();
        }


        private void OnCurrentLayoutContentViewModelChanged()
        {
            if (CurrentContentViewModel is DefaultViewModelBase)
            {
                NavigateDefaultViewConstituents();
            }
            else if (CurrentContentViewModel is SettingsViewModelBase)
            {
                NavigateSettingsViewConstituents();
            }
            else if (CurrentContentViewModel is null)
            {
                NavigateDefaultView();
            }

            OnPropertyChanged(nameof(CurrentContentViewModel));
        }


        private void OnCurrentNavigationBarViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentNavigationBarViewModel));
        }


        private void OnCurrentSideContentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSideContentViewModel));
        }


        private void OnInfoUpdated(object? sender, string info)
        {
            InfoText = info;
        }
    }
}
