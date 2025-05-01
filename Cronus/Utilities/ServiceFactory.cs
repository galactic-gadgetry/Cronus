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
                case "daily timesheet":
                    return new LayoutNavigationService<DailyTimesheetViewModel>(
                        navigationStore,
                        () => new DailyTimesheetViewModel(bookStore, navigationStore));
                case "edit book details":
                    return new LayoutNavigationService<EditBookDetailsViewModels>(
                        navigationStore,
                        () => new EditBookDetailsViewModels(bookStore, navigationStore));
                case "edit project details":
                    return new LayoutNavigationService<EditProjectDetailsViewModel>(
                        navigationStore,
                        () => new EditProjectDetailsViewModel(bookStore, navigationStore));
                case "layout":
                    return new NavigationService<LayoutViewModel>(
                        navigationStore,
                        () => new LayoutViewModel(bookStore, navigationStore));
                case "nav bar":
                    return new NavBarNavigationService<NavigationBarViewModel>(
                        navigationStore,
                        () => new NavigationBarViewModel(bookStore, navigationStore));
                case "null side content":
                    return new SideContentNavigationService<XButtonSideBarViewModel>(
                        navigationStore,
                        () => null);
                case "previous":
                    return new PreviousLayoutContentNavigationService(navigationStore);
                case "previous default view":
                    return new PreviousDefaultLayoutContentNavigationService(
                        navigationStore);
                case "project details":
                    return new LayoutNavigationService<ProjectDetailsViewModel>(
                        navigationStore,
                        () => new ProjectDetailsViewModel(bookStore, navigationStore));
                case "projects":
                    return new LayoutNavigationService<ProjectsViewModel>(
                        navigationStore,
                        () => new ProjectsViewModel(bookStore, navigationStore));
                case "saved books":
                    return new LayoutNavigationService<SavedBooksViewModel>(
                        navigationStore,
                        () => new SavedBooksViewModel(bookStore, navigationStore));
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
                        () => new XButtonSideBarViewModel(bookStore, navigationStore));
                default:
                    throw new NotImplementedException();
                    }
        }
    }
}
