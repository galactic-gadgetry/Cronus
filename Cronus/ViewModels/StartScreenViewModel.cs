using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models.DTOs;
using Cronus.Services;
using Cronus.Stores;
using Cronus.UIComponents.Dialogs;
using Cronus.Utilities;

namespace Cronus.ViewModels
{
    public class StartScreenViewModel : ViewModelBase
    {
        /// <summary>
        /// Used to navigate the Layout content to the Book Details
        /// view.
        /// </summary>
        private readonly INavigate _bookDetailsNavigationService;

        /// <summary>
        /// Used by the application manage the current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Main content to the Layout UI
        /// component.
        /// </summary>
        private readonly INavigate _layoutNavigationService;

        /// <summary>
        /// Used by the application to determine the app's navigation
        /// state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Saved Books view.
        /// </summary>
        private readonly INavigate _saveBooksNavigationService;


        /// <summary>
        /// Executed when the Load Existing Log Book button is
        /// clicked.
        /// </summary>
        public ICommand LoadExistingLogBookButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the New Log Book button is clicked.
        /// </summary>
        public ICommand NewLogBookButtonClickedCommand { get; }



        public StartScreenViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            LoadExistingLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnLoadExistingLogBookButtonClicked));
            NewLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNewLogBookButtonClicked));

            _bookDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "Book Details",
                    _bookStore,
                    _navigationStore);
            _layoutNavigationService =
                ServiceFactory.CreateNavigationService(
                    "layout",
                    _bookStore,
                    _navigationStore);
            _saveBooksNavigationService =
                ServiceFactory.CreateNavigationService(
                    "saved books",
                    _bookStore,
                    _navigationStore);
        }


        /// <summary>
        /// Usese information from the dialog window to create a new
        /// <see cref="Book"/> instance.
        /// </summary>
        /// <param name="dlg">Create New Log Book dialog window</param>
        private void CreateNewLogBookRequested(CreateNewLogBookDialog dlg)
        {
            // Create a new book from the dialog inputs and set it
            // as the book store's current book.
            BookDTO dto = new() { Name = dlg.NameText };
            BookService.CreateNewCurrentBook(_bookStore, dto);
            BookService.SaveCurrentBookToJson(_bookStore);

            // Update info bar.
            if (!_bookStore.CurrentBook.IsBookVoid)
            {
                OnInfoUpdated("New log book " +
                    $"'{_bookStore.CurrentBook.Name}' create");
            }
        }

        /// <summary>
        /// Navigates the view to the Book Details view.
        /// </summary>
        private void NavigateBookDetailsView()
        {
            _layoutNavigationService.Navigate();
            _bookDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Load Existing Log Book button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnLoadExistingLogBookButtonClicked(object? obj)
        {
            _layoutNavigationService.Navigate();
            _saveBooksNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the New Log Book button clicked event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnNewLogBookButtonClicked(object? obj)
        {
            CreateNewLogBookDialog dlg = DialogService.PromptUserWithCreateNewLogBookDialog();

            if (dlg.DialogResult == true)
            {
                CreateNewLogBookRequested(dlg);
            }

            // Navigate to the Book Details view.
            NavigateBookDetailsView();
        }
    }
}
