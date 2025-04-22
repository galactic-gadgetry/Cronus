using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;

namespace Cronus.ViewModels
{
    public class SettingsNavigationBarViewModel : ViewModelBase
    {

        // Backing Fields
        private bool isBookDetailsTabSelected = false;



        public bool IsBookDetailsTabSelected
        {
            get => isBookDetailsTabSelected;
            set
            {
                isBookDetailsTabSelected = value;
                OnPropertyChanged(nameof(IsBookDetailsTabSelected));
            }
        }



        public ICommand BookDetailsButtonClickedCommand { get; }



        public SettingsNavigationBarViewModel()
        {
            BookDetailsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookDetailsButtonClicked));
        }



        private void OnBookDetailsButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
