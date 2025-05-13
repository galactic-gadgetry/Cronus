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
using Cronus.Utilities;


namespace Cronus.ViewModels
{
    public class ProjectDetailsViewModel : SettingsViewModelBase
    {
        /// <summary>
        /// Used to manage the book store's current book.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to navigate to the Edit Project Details view.
        /// </summary>
        private readonly INavigate _editProjectDetailsNavigationService;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Projects view.
        /// </summary>
        private readonly INavigate _projectsNavigationService;


        /// <summary>
        /// Returns the <see cref="BookStore.CurrentFocusedProject"/>
        /// property.
        /// </summary>
        public Project? FocusedProject => _bookStore.CurrentFocusedProject;


        /// <summary>
        /// Executed when the Archive Project button is clicked.
        /// </summary>
        public ICommand ArchiveProjectButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Back To All Projects button is clicked.
        /// </summary>
        public ICommand BackToAllProjectsHyperlinkClickedCommand { get; }

        /// <summary>
        /// Executed when the Delete Project button is clicked.
        /// </summary>
        public ICommand DeleteProjectButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Edit Project button is clicked.
        /// </summary>
        public ICommand EditProjectButtonClickedCommand { get; }



        public ProjectDetailsViewModel(BookStore bookStore,
            NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            ArchiveProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnArchiveProjectButtonClicked));
            BackToAllProjectsHyperlinkClickedCommand = new RelayCommand(
                new Action<object?>(OnBackToAllProjectsHyperlinkClicked));
            DeleteProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnDeleteProjectButtonClicked));
            EditProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnEditProjectButtonClicked));

            _editProjectDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "edit project details",
                    _bookStore,
                    _navigationStore);
            _projectsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "projects",
                    _bookStore,
                    _navigationStore);
        }


        /// <summary>
        /// Removes the project from the book store's current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        private void DeleteProjectRequested()
        {
            ArgumentNullException.ThrowIfNull(FocusedProject, nameof(FocusedProject));

            BookService.DeleteProjectFromCurrentBook(_bookStore, FocusedProject);

            // Save the book and update the info label.
            BookService.SaveCurrentBookToJson(_bookStore);
            OnInfoUpdated($"Project '{FocusedProject.Name}' deleted");

            // Navigate to the projects view.
            _projectsNavigationService.Navigate();
        }


        private void OnArchiveProjectButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Back To All Projects button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnBackToAllProjectsHyperlinkClicked(object? obj)
        {
            _projectsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Delete Project button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void OnDeleteProjectButtonClicked(object? obj)
        {
            if (FocusedProject == null)
            {
                throw new NullReferenceException("Focused project cannot be " +
                    "null");
            }

            if (DialogService.PromptUserWithDeleteConfirmationDialog(
                FocusedProject).DialogResult == true)
            {
                DeleteProjectRequested();
            }
        }

        /// <summary>
        /// Handles the Edit Project button click event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnEditProjectButtonClicked(object? obj)
        {
            // There should already be a focused project, so
            // navigate to the Edit Project Details view.
            _editProjectDetailsNavigationService.Navigate();
        }
    }
}
