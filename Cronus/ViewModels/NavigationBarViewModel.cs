using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Services;
using Cronus.Stores;
using Cronus.UIComponents.Dialogs;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
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
        /// Used to navigate to the Edit Book Details view.
        /// </summary>
        private readonly INavigate _editBookDetailsNavigationService;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Projects view.
        /// </summary>
        private readonly INavigate _projectsNavigationService;

        /// <summary>
        /// Used to navigate to the Start Screen view.
        /// </summary>
        private readonly INavigate _startScreenNavigationService;


        // Backing Fields
        private bool isBookBarPopupOpen = false;
        private bool isProjectsTabSelected = false;


        /// <summary>
        /// Returns the book store's current book.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;

        /// <summary>
        /// Returns the book store's current book's
        /// <see cref="Book.Name"/> property.
        /// </summary>
        public string CurrentBookName =>
            CurrentBook.IsBookVoid ? "No Active Book" : CurrentBook.Name;

        /// <summary>
        /// Returns the book store's current book's
        /// <see cref="Book.StatusText"/> property.
        /// </summary>
        public string CurrentBookStatus => CurrentBook.StatusText;

        /// <summary>
        /// True if the Book Bar's popup is open, false otherwise.
        /// </summary>
        public bool IsBookBarPopupOpen
        {
            get => isBookBarPopupOpen;
            set
            {
                isBookBarPopupOpen = value;
                OnPropertyChanged(nameof(IsBookBarPopupOpen));
            }
        }

        /// <summary>
        /// True if the Projects tab is selected, false otherwise.
        /// </summary>
        public bool IsProjectsTabSelected
        {
            get => isProjectsTabSelected;
            set
            {
                isProjectsTabSelected = value;
                OnPropertyChanged(nameof(IsProjectsTabSelected));
            }
        }


        /// <summary>
        /// Executed when the Book Bar's Name button is clicked.
        /// </summary>
        public ICommand BookBarBookNameButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Book Bar's Save Book button is clicked.
        /// </summary>
        public ICommand BookBarSaveBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Book Bar's Settings button is clicked.
        /// </summary>
        public ICommand BookBarSettingsButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Book Controls' Popup's Close Book button
        /// is clicked.
        /// </summary>
        public ICommand BookControlsPopupCloseBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Book Controls' Popup's Edit Book button
        /// is clicked.
        /// </summary>
        public ICommand BookControlsPopupEditBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Book Controls' Switch Books button is
        /// clicked.
        /// </summary>
        public ICommand BookControlsPopupSwitchBooksButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Projects button is clicked.
        /// </summary>
        public ICommand ProjectsButtonClickedCommand { get; }



        public NavigationBarViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            BookBarBookNameButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookBarBookNameButtonClicked));
            BookBarSaveBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookBarSaveBookButtonClicked));
            BookBarSettingsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookBarSettingsButtonClicked));
            BookControlsPopupCloseBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookControlsPopupCloseBookButtonClicked));
            BookControlsPopupEditBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookControlsPopupEditBookButtonClicked));
            BookControlsPopupSwitchBooksButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBookControlsPopupSwitchBooksButtonClicked));

            ProjectsButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectsButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "book details",
                    _bookStore,
                    _navigationStore);
            _editBookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "edit book details",
                    _bookStore,
                    _navigationStore);
            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "projects",
                    _bookStore,
                    _navigationStore);
            _startScreenNavigationService =
                ServiceFactory.CreateNavigationService(
                    "start screen",
                    _bookStore,
                    _navigationStore);

            SetSelectedTab();
        }


        /// <summary>
        /// Sets the <seealso cref="IsBookBarPopupOpen"/> property
        /// to false.
        /// </summary>
        private void CloseBookBarPopup()
        {
            IsBookBarPopupOpen = false;
        }

        /// <summary>
        /// Sets the book store's current book to a new
        /// <see cref="Book"/> instance and navigates to the Book
        /// Details view.
        /// </summary>
        /// <param name="dlg"></param>
        private void CreateNewBookRequested(CreateNewLogBookDialog dlg)
        {
            // Create a new book from the dialog inputs and set
            // it as the book store's current book.
            BookDTO dto = new() { Name = dlg.NameText };
            BookService.CreateNewCurrentBook(_bookStore, dto);
            BookService.SaveCurrentBookToJson(_bookStore);

            // Navigate to the Book Details view.
            _bookDetailsNavigationService.Navigate();

            // Update the info bar.
            if (!_bookStore.CurrentBook.IsBookVoid)
            {
                OnInfoUpdated("New log book " +
                    $"{_bookStore.CurrentBook.Name}' created");
            }
        }


        private void DeselectAllTabs()
        {
            IsProjectsTabSelected = false;
        }

        /// <summary>
        /// Handles the Book Bar's Name button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBookBarBookNameButtonClicked(object? obj)
        {
            OpenBookBarPopup();
        }

        /// <summary>
        /// Handles the Book Bar's Save Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnBookBarSaveBookButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Book Bar's Settings button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBookBarSettingsButtonClicked(object? obj)
        {
            _bookDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Book Controls' Popup's Close Book button
        /// click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBookControlsPopupCloseBookButtonClicked(object? obj)
        {
            CloseBookBarPopup();
            BookService.CloseCurrentBook(_bookStore);
            _startScreenNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Book Controls' Popup's Edit Book button
        /// click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBookControlsPopupEditBookButtonClicked(object? obj)
        {
            CloseBookBarPopup();
            _editBookDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Book Controls' Popup's Switch Books button
        /// click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBookControlsPopupSwitchBooksButtonClicked(object? obj)
        {
            OpenManageBooksDialog();
        }

        /// <summary>
        /// Handles the Projects button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnProjectsButtonClicked(object? obj)
        {
            _projectsNavigationService.Navigate();
        }

        /// <summary>
        /// Sets the <seealso cref="IsBookBarPopupOpen"/> property to
        /// true.
        /// </summary>
        private void OpenBookBarPopup()
        {
            IsBookBarPopupOpen = true;
        }

        /// <summary>
        /// Displays and handles the Manage Books dialog.
        /// </summary>
        private void OpenManageBooksDialog()
        {
            ManageBooksDialog manageBooksDlg =
                DialogService.PromptUserWithManageBooksDialog(
                    _bookStore, _navigationStore);

            // If the Manage Book Dialog returns true, the user wishes
            // to create a new log book.
            if (manageBooksDlg.DialogResult == true)
            {
                // Display the Create New Log Book dialog and handle the result.
                CreateNewLogBookDialog createNewBookDlg =
                    DialogService.PromptUserWithCreateNewLogBookDialog();

                if (createNewBookDlg.DialogResult == true)
                {
                    CreateNewBookRequested(createNewBookDlg);
                }
                else if (createNewBookDlg.DialogResult == false)
                {
                    OpenManageBooksDialog();
                }
            }
        }


        private void SetSelectedTab()
        {
            DeselectAllTabs();

            switch (_navigationStore.CurrentLayoutContentViewModel)
            {
                case ProjectsViewModel:
                    IsProjectsTabSelected = true;
                    break;
                default:
                    return;
            }
        }
    }
}
