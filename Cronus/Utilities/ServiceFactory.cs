using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Services;
using Cronus.Stores;
using Cronus.ViewModels;

namespace Cronus.Utilities
{
    public static class ServiceFactory
    {

        public static INavigate CreateNavigationService(string type,
            BookStore bookStore, NavigationStore navigationStore)
        {
            switch (type.ToLower())
            {
                case "book details":
                    return new LayoutNavigationService<BookDetailsViewModel>(
                        navigationStore,
                        () => new BookDetailsViewModel(bookStore, navigationStore));
                case "layout":
                    return new NavigationService<LayoutViewModel>(
                        navigationStore,
                        () => new LayoutViewModel(bookStore, navigationStore));
                case "settings nav bar":
                    return new NavBarNavigationService<SettingsNavigationBarViewModel>(
                        navigationStore,
                        () => new SettingsNavigationBarViewModel());
                case "start screen":
                    return new NavigationService<StartScreenViewModel>(
                        navigationStore,
                        () => new StartScreenViewModel(bookStore, navigationStore));
                case "x button side bar":
                    return new SideContentNavigationService<XButtonSideBarViewModel>(
                        navigationStore,
                        () => new XButtonSideBarViewModel());
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
