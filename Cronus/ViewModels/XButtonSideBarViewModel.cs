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
        /// <summary>
        /// Used to navigate to the previous default view.
        /// </summary>
        private readonly INavigate _previousDefaultViewNavigationService;


        /// <summary>
        /// Executed when the X button is clicked.
        /// </summary>
        public ICommand XButtonClickedCommand { get; }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="XButtonSideBarViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public XButtonSideBarViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _previousDefaultViewNavigationService =
                ServiceFactory.CreateNavigationService(
                    "previous default view",
                    bookStore,
                    navigationStore);

            XButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnXButtonClicked));
        }


        /// <summary>
        /// Handles the X button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnXButtonClicked(object? obj)
        {
            _previousDefaultViewNavigationService.Navigate();
        }
    }
}
