using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.ViewModels;

namespace Cronus.Stores
{
    public class NavigationStore
    {
        // Backing Fields
        private ViewModelBase? currentLayoutContentViewModel;
        private ViewModelBase? currentMainContentViewModel;
        private ViewModelBase? currentNavigationBarViewModel;
        private ViewModelBase? currentSideContentViewModel;


        /// <summary>
        /// View-model used to determine the <see cref="Layout"/>'s
        /// content.
        /// </summary>
        public ViewModelBase? CurrentLayoutContentViewModel
        {
            get => currentLayoutContentViewModel;
            set
            {
                // Track the view that is being navigated away from
                // for return functions.
                if (CurrentLayoutContentViewModel is DefaultViewModelBase)
                {
                    PreviousDefaultLayoutContentViewModel =
                        (DefaultViewModelBase)CurrentLayoutContentViewModel;
                }
                PreviousLayoutContentViewModel = CurrentLayoutContentViewModel;

                currentLayoutContentViewModel = value;
                OnCurrentLayoutContentViewModelChanged();
            }
        }

        /// <summary>
        /// View-model used to determine the <see cref="MainView"/>'s
        /// content.
        /// </summary>
        public ViewModelBase? CurrentMainContentViewModel
        {
            get => currentMainContentViewModel;
            set
            {
                currentMainContentViewModel = value;
                OnCurrentMainContentViewModelChanged();
            }
        }

        /// <summary>
        /// View-model used to determine the Navigation Bar's content.
        /// </summary>
        public ViewModelBase? CurrentNavigationBarViewModel
        {
            get => currentNavigationBarViewModel;
            set
            {
                currentNavigationBarViewModel = value;
                OnCurrentNavigationBarViewModelChanged();
            }
        }

        /// <summary>
        /// View-model used to determine the Side content area's
        /// content.
        /// </summary>
        public ViewModelBase? CurrentSideContentViewModel
        {
            get => currentSideContentViewModel;
            set
            {
                currentSideContentViewModel = value;
                OnCurrentSideContentViewModelChanged();
            }
        }

        /// <summary>
        /// Used to determine the previous
        /// <see cref="LayoutViewModel.CurrentContnentViewModel"/>.
        /// </summary>
        public DefaultViewModelBase? PreviousDefaultLayoutContentViewModel { get; set; }


        public ViewModelBase? PreviousLayoutContentViewModel { get; set; }


        /// <summary>
        /// Raised when the
        /// <seealso cref="CurrentLayoutContentViewModel"/> is set.
        /// </summary>
        public Action? CurrentLayoutContentViewModelChanged;

        /// <summary>
        /// Raised when the
        /// <seealso cref="CurrentMainContentViewModel"/> is set.
        /// </summary>
        public Action? CurrentMainContentViewModelChanged;

        /// <summary>
        /// Raised when the
        /// <seealso cref="CurrentNavigationBarViewModel"/> is set.
        /// </summary>
        public Action? CurrentNavigationBarViewModelChanged;

        /// <summary>
        /// Raised when the
        /// <seealso cref="CurrentSideContentViewModel"/> is set.
        /// </summary>
        public Action? CurrentSideContentViewModelChanged;


        public event EventHandler<string>? InfoUpdated;


        /// <summary>
        /// Handles the setting of the current layout content
        /// view-model.
        /// </summary>
        private void OnCurrentLayoutContentViewModelChanged()
        {
            if (CurrentLayoutContentViewModel != null)
            {
                CurrentLayoutContentViewModel.InfoUpdated +=
                    OnInfoUpdated;
            }
            CurrentLayoutContentViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Handles the setting of the current main content
        /// view-model.
        /// </summary>
        private void OnCurrentMainContentViewModelChanged()
        {
            if (CurrentMainContentViewModel != null)
            {
                CurrentMainContentViewModel.InfoUpdated +=
                    OnInfoUpdated;
            }
            CurrentMainContentViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Handles the setting of the current navigation bar
        /// view-model.
        /// </summary>
        private void OnCurrentNavigationBarViewModelChanged()
        {
            CurrentNavigationBarViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Handles the setting of the current side content
        /// view-model.
        /// </summary>
        private void OnCurrentSideContentViewModelChanged()
        {
            CurrentSideContentViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Handles the view-models'
        /// <see cref="ViewModelBase.InfoUpdated"/> event being
        /// raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="info"></param>
        public void OnInfoUpdated(object? sender, string info)
        {
            InfoUpdated?.Invoke(this, info);
        }
    }
}
