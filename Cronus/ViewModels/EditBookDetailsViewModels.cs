using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Services;
using Cronus.Stores;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class EditBookDetailsViewModels : SettingsViewModelBase
    {
        /// <summary>
        /// Used to navigate to the Book Details view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used to manage the book store's current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;


        // Backing Fields
        private string nameText = string.Empty;


        /// <summary>
        /// Returns true if the input fields are valid, false
        /// otherwise.
        /// </summary>
        public bool AreInputsValid
        {
            get
            {
                return !string.IsNullOrEmpty(nameText);
            }
        }

        /// <summary>
        /// Returns the book store's current book.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;


        /// <summary>
        /// Text for the Name text box.
        /// </summary>
        public string NameText
        {
            get => nameText;
            set
            {
                nameText = value;
                OnPropertyChanged(nameof(NameText));
                OnPropertyChanged(nameof(AreInputsValid));
            }
        }


        /// <summary>
        /// Executed when the Back button is clicked.
        /// </summary>
        public ICommand BackButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Save button is clicked.
        /// </summary>
        public ICommand SaveButtonClickedCommand { get; }


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="EditBookDetailsViewModels"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public EditBookDetailsViewModels(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            NameText = CurrentBook.Name;

            BackButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBackButtonClicked));
            SaveButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSaveButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details",
                    _bookStore,
                    _navigationStore);
        }


        /// <summary>
        /// Handles the Back button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBackButtonClicked(object? obj)
        {
            _bookDetailsNavigationService.Navigate();
        }


        /// <summary>
        /// Handles the Save button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnSaveButtonClicked(object? obj)
        {
            // Update details of the data transfer object.
            BookDTO dto = new()
            {
                Name = NameText,
            };

            // Update the book details and save to file.
            BookService.EditBookDetails(dto, CurrentBook);
            BookService.SaveCurrentBookToJson(_bookStore);

            OnInfoUpdated("Book details updated");

            _bookDetailsNavigationService.Navigate();
        }
    }
}
