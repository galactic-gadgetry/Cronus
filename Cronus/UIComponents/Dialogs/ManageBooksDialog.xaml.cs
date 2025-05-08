using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Services;
using Cronus.Stores;
using Cronus.Utilities;

namespace Cronus.UIComponents.Dialogs
{
    /// <summary>
    /// Interaction logic for ManageBooksDialog.xaml
    /// </summary>
    public partial class ManageBooksDialog : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Used to navigate to the Book Details view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used by the application manage the current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigate state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        private readonly INavigate _nullLayoutNavigationService;


        private readonly INavigate _nullNavBarNavigationService;

        /// <summary>
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;


        // Backing Fields
        private bool isSavedBookCardMoreButtonPopupOpen = false;
        private string savedBookCardMoreButtonPopupButtonText = string.Empty;
        private ObservableCollection<BookHeader>? savedBooks;


        /// <summary>
        /// True if the selected saved book in the Saved Books list
        /// is the book store's current book.
        /// </summary>
        private bool isSavedBookCardMoreButtonPopupSelectedHeaderCurrent = false;

        /// <summary>
        /// Book header selected by the user.
        /// </summary>
        private BookHeader? savedBookCardMoreButtonPopupSelectedHeader = null;


        /// <summary>
        /// True if the Saved Book Card More button popup is open.
        /// </summary>
        public bool IsSavedBookCardMoreButtonPopupOpen
        {
            get => isSavedBookCardMoreButtonPopupOpen;
            set
            {
                isSavedBookCardMoreButtonPopupOpen = value;
                OnPropertyChanged(nameof(IsSavedBookCardMoreButtonPopupOpen));
            }
        }

        /// <summary>
        /// Text for the Saved Book Card More button popup.
        /// </summary>
        public string SavedBookCardMoreButtonPopupButtonText
        {
            get => savedBookCardMoreButtonPopupButtonText;
            set
            {
                savedBookCardMoreButtonPopupButtonText = value;
                OnPropertyChanged(nameof(SavedBookCardMoreButtonPopupButtonText));
            }
        }

        /// <summary>
        /// Collection of <see cref="BookHeader"/> instances
        /// associated with the saved <see cref="Book"/> objects
        /// found in the save directory.
        /// </summary>
        public ObservableCollection<BookHeader>? SavedBooks
        {
            get => savedBooks;
            set
            {
                savedBooks = value;
                OnPropertyChanged(nameof(SavedBooks));
            }
        }


        /// <summary>
        /// Executed when the Saved Book Card's More button popup's
        /// button is clicked.
        /// </summary>
        public ICommand SavedBookCardMoreButtonPopupButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Saved Book Card's More button is
        /// clicked.
        /// </summary>
        public ICommand SavedBookMoreButtonClickedCommand { get; }


        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="ManageBooksDialog"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public ManageBooksDialog(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;
            SavedBooks = GetSavedBookHeaders();
            DataContext = this;

            SavedBookCardMoreButtonPopupButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardMoreButtonPopupButtonClicked));
            SavedBookMoreButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSavedBookCardMoreButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details",
                    _bookStore,
                    _navigationStore);
            _nullLayoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "null layout",
                    _bookStore,
                    _navigationStore);
            _nullNavBarNavigationService =
                ServiceFactory.CreateNavigationService(
                    "null nav bar",
                    _bookStore,
                    _navigationStore);
            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen",
                    _bookStore,
                    _navigationStore);

            InitializeComponent();
        }


        /// <summary>
        /// Handles the <seealso cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Handles the Create Log Book button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateLogBookButton_Click(object sender, EventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Closes the current book and navigates to the Start Screen
        /// view.
        /// </summary>
        private void CloseSelectedBookRequested()
        {
            // The book service's CloseCurrentBook method returns
            // true if the book was closed, false otherwise.
            if (BookService.CloseCurrentBook(_bookStore))
            {
                //Navigate to the Start Screen.
                _startScreenNavigationService.Navigate();


                // Close the ManageBooks dialog.
                DialogResult = false;
            }
        }

        /// <summary>
        /// Returns a collection of Book Headers associated to the
        /// saved <see cref="Book"/> object files found in the
        /// save directory.
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<BookHeader> GetSavedBookHeaders()
        {
            ObservableCollection<BookHeader> headers = BookService.GetSavedBookHeaders();

            // Move the current book to the top of the list.
            BookHeader currentHeader = headers
                .Single(h => h.ID == _bookStore.CurrentBook.ID);
            headers.Remove(currentHeader);
            headers.Insert(0, currentHeader);

            return headers;
        }

        /// <summary>
        /// Handles the Saved Bokk Card's More button popup's button
        /// click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void OnSavedBookCardMoreButtonPopupButtonClicked(object? obj)
        {
            if (savedBookCardMoreButtonPopupSelectedHeader == null)
            {
                throw new NullReferenceException("Selected header " +
                    "cannot be null");
            }

            if (isSavedBookCardMoreButtonPopupSelectedHeaderCurrent)
            {
                CloseSelectedBookRequested();
            }
            else
            {
                OpenSelectedBookRequested(savedBookCardMoreButtonPopupSelectedHeader);
            }
        }

        /// <summary>
        /// Handles the Saved Book Card's More button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void OnSavedBookCardMoreButtonClicked(object? obj)
        {
            BookHeader? header = obj as BookHeader;
            if (header == null)
            {
                throw new InvalidOperationException("The caller must be " +
                    "a BookHeader object");
            }

            if (header.ID == _bookStore.CurrentBook.ID)
            {
                SavedBookCardMoreButtonPopupButtonText = "Close book";
                isSavedBookCardMoreButtonPopupSelectedHeaderCurrent = true;
            }
            else
            {
                SavedBookCardMoreButtonPopupButtonText = "Open book";
                isSavedBookCardMoreButtonPopupSelectedHeaderCurrent = false;
            }

            // Set the selected header.
            savedBookCardMoreButtonPopupSelectedHeader = header;

            OpenSavedBookCardMoreButtonPopup();
        }

        /// <summary>
        /// Sets the <see cref="IsSavedBookCardMoreButtonPopupOpen"/>
        /// property to true.
        /// </summary>
        private void OpenSavedBookCardMoreButtonPopup()
        {
            IsSavedBookCardMoreButtonPopupOpen = true;
        }

        /// <summary>
        /// Closes the book store's current book and loads the
        /// selected saved book as the current book.
        /// </summary>
        /// <param name="header"></param>
        private void OpenSelectedBookRequested(BookHeader header)
        {
            // Close the current book.
            _ = BookService.CloseCurrentBook(_bookStore);

            // Open the selected book.
            BookService.LoadBookToBookStoreFromJson(_bookStore, header.BookSaveFilePath);

            // First, null out the views so that the old views
            // are not kept alive.
            // Navigate to the Book Details view.
            _nullLayoutNavigationService.Navigate();
            _nullNavBarNavigationService.Navigate();
            _bookDetailsNavigationService.Navigate();

            // Close the Manage Books Dialog.
            DialogResult = false;
        }

        /// <summary>
        /// Handles the X button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
