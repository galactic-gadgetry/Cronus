using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Services;
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


        public static BookStore LoadBookStoreFromFile(string filePath)
        {
            BookStore bookStore = CreateBookStore();
            BookService.LoadBookToBookStoreFromJson(bookStore, filePath);

            return bookStore;
        }
    }
}
