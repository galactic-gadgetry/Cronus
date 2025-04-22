using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Services;
using Cronus.Stores;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class XButtonSideBarViewModel : ViewModelBase
    {

        private readonly INavigate _projectsNavigationService;



        public ICommand XButtonClickedCommand { get; }



        public XButtonSideBarViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "projects",
                    bookStore,
                    navigationStore);

            XButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnXButtonClicked));
        }



        private void OnXButtonClicked(object? obj)
        {
            _projectsNavigationService.Navigate();
        }
    }
}
