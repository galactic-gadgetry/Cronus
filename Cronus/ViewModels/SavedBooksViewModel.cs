using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Services;
using Cronus.Stores;

namespace Cronus.ViewModels
{
    public class SavedBooksViewModel: ViewModelBase
    {

        private readonly BookStore _bookStore;



        public ObservableCollection<BookHeader> SavedBooks { get; set; }



        public ICommand SavedBookCardDeleteButtonClickedCommand { get; }


        public ICommand SavedBookCardLoadButtonClickedCommand { get; }



        public SavedBooksViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;

            SavedBooks = BookService.GetSavedBookHeaders();

            SavedBookCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardDeleteButtonClicked));
            SavedBookCardLoadButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardLoadButtonClicked));
        }



        private void DeleteBookRequested(BookHeader header)
        {
            BookService.DeleteBook(_bookStore, header);

            // Remove the header from the collection.
            SavedBooks.Remove(header);
        }


        private void OnSavedBookCardDeleteButtonClicked(object? obj)
        {
            BookHeader? header = obj as BookHeader;
            if (header == null)
            {
                throw new InvalidOperationException("The caller must be " +
                    "a BookHeader object");
            }

            // If the dialog was accepted, delete the selected book.
            if (DialogService.PromptUserWithDeleteConfirmationDialog(
                _bookStore.CurrentBook).DialogResult == true)
            {
                DeleteBookRequested(header);
            }
        }


        private void OnSavedBookCardLoadButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
