using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Stores;

namespace Cronus.Utilities
{
    public static class StoreFactory
    {

        public static BookStore CreateBookStore()
        {
            return new BookStore();
        }


        public static NavigationStore CreateNavigationStore()
        {
            return new NavigationStore();
        }
    }
}
