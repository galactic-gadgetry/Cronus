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
                case "start screen":
                    return new NavigationService<StartScreenViewModel>(
                        navigationStore,
                        () => new StartScreenViewModel(bookStore));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
