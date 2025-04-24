using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Stores;

namespace Cronus.Services
{
    public class PreviousDefaultLayoutContentNavigationService : INavigate
    {
        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="PreviousDefaultLayoutContentNavigationService"/>
        /// class.
        /// </summary>
        /// <param name="navigationStore"></param>
        public PreviousDefaultLayoutContentNavigationService(
            NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }


        /// <summary>
        /// Sets the navigation store's
        /// <see cref="NavigationStore.CurrentLayoutContentViewModel"/>
        /// property to the
        /// <see cref="NavigationStore.PreviousDefaultLayoutContentViewModel"/>
        /// view-model.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentLayoutContentViewModel =
                _navigationStore.PreviousDefaultLayoutContentViewModel;
        }
    }
}
