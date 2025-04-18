using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Stores;
using Cronus.ViewModels;

namespace Cronus.Services
{
    public class NavBarNavigationService<TViewModel> : INavigate
        where TViewModel : ViewModelBase
    {
        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Callback used to create the new view-model.
        /// </summary>
        private readonly Func<TViewModel> _createViewModel;


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="NavBarNavigationService"/> class.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="createViewModel"></param>
        public NavBarNavigationService(
            NavigationStore navigationStore,
            Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }


        /// <summary>
        /// Sets the <seealso cref="_navigationStore"/>'s
        /// navigation bar view-model property to the view-model
        /// created by the <seealso cref="_createViewModel"/>
        /// callback.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentNavigationBarViewModel = _createViewModel();
        }
    }
}
