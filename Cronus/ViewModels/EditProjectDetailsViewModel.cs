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
    public class EditProjectDetailsViewModel : SettingsViewModelBase
    {
        /// <summary>
        /// Used to manage the current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the previous layout content view.
        /// </summary>
        private readonly INavigate _previousLayoutContentNavigationService;

        /// <summary>
        /// Used to navigate to the Projects view.
        /// </summary>
        private readonly INavigate _projectsNavigationService;


        // Backing Fields
        private string codeText = string.Empty;
        private string nameText = string.Empty;
        private string wbsText = string.Empty;


        /// <summary>
        /// Return true if the input fields are valid, false
        /// otherwise.
        /// </summary>
        public bool AreInputsValid
        {
            get
            {
                if (string.IsNullOrEmpty(codeText) ||
                    string.IsNullOrEmpty(nameText) ||
                    string.IsNullOrEmpty(wbsText))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Text for the Code text box.
        /// </summary>
        public string CodeText
        {
            get => codeText;
            set
            {
                codeText = value;
                OnPropertyChanged(nameof(CodeText));
                OnPropertyChanged(nameof(AreInputsValid));
            }
        }

        /// <summary>
        /// Returns the book store's
        /// <see cref="BookStore.CurrentFocusedProject"/> property.
        /// </summary>
        public Project? FocusedProject => _bookStore.CurrentFocusedProject;

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
        /// Text for the WBS text box.
        /// </summary>
        public string WbsText
        {
            get => wbsText;
            set
            {
                wbsText = value;
                OnPropertyChanged(nameof(WbsText));
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
        /// <see cref="EditProjectDetailsViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public EditProjectDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            BackButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnBackButtonClicked));
            SaveButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnSaveButtonClicked));

            _previousLayoutContentNavigationService =
                ServiceFactory.CreateNavigationService(
                    "previous",
                    _bookStore,
                    _navigationStore);
            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "projects",
                    _bookStore,
                    _navigationStore);

            ResetInputFields();
        }


        /// <summary>
        /// Edits the project details and navigates to the previous
        /// view if able, displays an error dialog otherwise.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if the
        /// FocusedProject is null</exception>
        private void EditProjectDetailsRequested()
        {
            if (FocusedProject == null)
            {
                throw new NullReferenceException("FocusedProject " +
                    "must not be null");
            }

            ProjectDTO dto = new()
            {
                Code = CodeText,
                Name = NameText,
                Wbs = WbsText,
            };

            (bool result, string? detail) =
                ProjectService.EditProjectDetails(_bookStore,
                FocusedProject, dto);
            if (result)
            {
                // Save the book and navigate to the previous view.
                BookService.SaveCurrentBookToJson(_bookStore);
                _projectsNavigationService.Navigate();
            }
            else
            {
                string caption = "Unable to Edit Project";
                string message = $"The selected {detail} is assigned " +
                    $"to another project. A project's {detail} must " +
                    "be unique.";
                DialogService.PromptUserWithErrorMessageWithOKButtonDialog(caption, message);
            }
        }

        /// <summary>
        /// Handles the Back button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBackButtonClicked(object? obj)
        {
            _previousLayoutContentNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Save button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnSaveButtonClicked(object? obj)
        {
            EditProjectDetailsRequested();
        }

        /// <summary>
        /// Sets the input fields to the
        /// <seealso cref="FocusedProject"/>'s properties.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if the
        /// FocusedProject property is null</exception>
        private void ResetInputFields()
        {
            if (FocusedProject == null)
            {
                throw new NullReferenceException("FocusedProject " +
                    "must not be null");
            }

            CodeText = FocusedProject.Code;
            NameText = FocusedProject.Name;
            WbsText = FocusedProject.Wbs;
        }
    }
}
