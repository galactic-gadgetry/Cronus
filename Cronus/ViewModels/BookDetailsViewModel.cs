using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;
using Cronus.Stores;

namespace Cronus.ViewModels
{
    public class BookDetailsViewModel : SettingsViewModelBase
    {

        private readonly BookStore _bookStore;


        private readonly NavigationStore _navigationStore;


        public Book CurrentBook => _bookStore.CurrentBook;



        public BookDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;
        }
    }
}
