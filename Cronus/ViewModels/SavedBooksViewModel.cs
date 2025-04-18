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
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class SavedBooksViewModel: ViewModelBase
    {
        /// <summary>
        /// Used to navigate to the Book Details view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used to manage the current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the layout UI
        /// component.
        /// </summary>
        private readonly INavigate _layoutNavigationService;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;


        /// <summary>
        /// Collection of book headers found in the save directory.
        /// </summary>
        public ObservableCollection<BookHeader> SavedBooks { get; set; }


        /// <summary>
        /// Executed when the Back button is clicked.
        /// </summary>
        public ICommand BackButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the saved book card's Delete button is
        /// clicked.
        /// </summary>
        public ICommand SavedBookCardDeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the saved book card's Load button is
        /// clicked.
        /// </summary>
        public ICommand SavedBookCardLoadButtonClickedCommand { get; }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="SavedBooksViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public SavedBooksViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            SavedBooks = BookService.GetSavedBookHeaders();

            BackButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBackButtonClicked));
            SavedBookCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardDeleteButtonClicked));
            SavedBookCardLoadButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardLoadButtonClicked));


            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details",
                    _bookStore,
                    _navigationStore);
            _layoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "layout",
                    _bookStore,
                    _navigationStore);
            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen",
                    _bookStore,
                    _navigationStore);

            // If no headers were loaded display a message to the
            // user in a dialog window.
            if (SavedBooks.Count == 0)
            {
                DisplayNoSavedBookDialog();
            }
        }


        /// <summary>
        /// Delete's the book's save file.
        /// </summary>
        /// <param name="header"></param>
        private void DeleteBookRequested(BookHeader header)
        {
            BookService.DeleteBook(_bookStore, header);

            // Remove the header from the collection.
            SavedBooks.Remove(header);
        }

        /// <summary>
        /// Displays the No Saved Book message dialog and navigates
        /// to the Start Screen view.
        /// </summary>
        private void DisplayNoSavedBookDialog()
        {
            string caption = "No saved books";
            string msg = "No saved books found in the save directory.";
            _ = DialogService.PromptUserWithMessageWithBackButtonDialog(caption, msg);

            // The Message with Back Button dialog only has a single "back"
            // button so we navigate back to the Start Screen view.
            _startScreenNavigationService.Navigate();
        }


        /// <summary>
        /// Loads a saved book instance from a JSON file and sets it
        /// as the book store's current book.
        /// </summary>
        /// <param name="header"></param>
        private void LoadBookFileRequested(BookHeader header)
        {
            _ = BookService.LoadBookToBookStoreFromJson(
                _bookStore, header.BookSaveFilePath);

            // Navigate to the Book Details view.
            _layoutNavigationService.Navigate();
            _bookDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Back button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBackButtonClicked(object? obj)
        {
            _startScreenNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the saved book card's Delete button click event.
        /// </summary>
        /// <param name="obj">SavedBookCard instance that raised the
        /// event</param>
        /// <exception cref="InvalidOperationException">Thrown if the calling
        /// object is not a BookHeader</exception>
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

        /// <summary>
        /// Handles the saved book card's Load button click event.
        /// </summary>
        /// <param name="obj">SavedBookCard instance that raised the
        /// event</param>
        /// <exception cref="InvalidOperationException">Thrown if the calling
        /// object is not a BookHeader</exception>
        private void OnSavedBookCardLoadButtonClicked(object? obj)
        {
            BookHeader? header = obj as BookHeader;
            if (header == null)
            {
                throw new InvalidOperationException("The caller must be " +
                    "a BookHeader object");
            }

            LoadBookFileRequested(header);
        }
    }
}
