using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Services;
using Cronus.Stores;
using Cronus.UIComponents.Dialogs;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class BookDetailsViewModel : SettingsViewModelBase
    {
        /// <summary>
        /// Used to manage the book store's current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Edit Book Details view.
        /// </summary>
        private readonly INavigate _editBookDetailsNavigationSerivce;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;

        /// <summary>
        /// Returns the book store's current book.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;



        /// <summary>
        /// Executed when the Archive Book button is clicked.
        /// </summary>
        public ICommand ArchiveBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Delete Book button is clicked.
        /// </summary>
        public ICommand DeleteBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Edit Book Details button is clicked.
        /// </summary>
        public ICommand EditBookDetailsButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Name Edit button is clicked.
        /// </summary>
        public ICommand NameEditButtonClickedCommand { get; }



        public BookDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            ArchiveBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnArchiveBookButtonClicked));
            DeleteBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnDeleteBookButtonClicked));
            EditBookDetailsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnEditBookDetailsButtonClicked));
            NameEditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNameEditButtonClicked));

            _editBookDetailsNavigationSerivce =
                ServiceFactory.CreateNavigationService(
                    "edit book details",
                    _bookStore,
                    _navigationStore);
            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen",
                    _bookStore,
                    _navigationStore);
        }


        /// <summary>
        /// Deletes the book store's current book and navigates
        /// to the Start Screen view.
        /// </summary>
        private void DeleteBookRequested()
        {
            BookService.DeleteCurrentBook(_bookStore);

            OnInfoUpdated("Log book deleted");

            _startScreenNavigationService.Navigate();   
        }


        private void OnArchiveBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Delete Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnDeleteBookButtonClicked(object? obj)
        {
            ConfirmationDialog dlg =
                DialogService.PromptUserWithDeleteConfirmationDialog(CurrentBook);

            if (dlg.DialogResult == true)
            {
                DeleteBookRequested();
            }
        }

        /// <summary>
        /// Handles the Edit Book Details button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnEditBookDetailsButtonClicked(object? obj)
        {
            _editBookDetailsNavigationSerivce.Navigate();
        }

        /// <summary>
        /// Handles the Name Edit button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnNameEditButtonClicked(object? obj)
        {
            _editBookDetailsNavigationSerivce.Navigate();
        }
    }
}
