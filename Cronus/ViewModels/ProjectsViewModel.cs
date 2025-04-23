using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class ProjectsViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the book store's current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Project Details view.
        /// </summary>
        private readonly INavigate _projectDetailsNavigationService;


        /// <summary>
        /// Returns the current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        public ObservableCollection<Project> Projects =>
            _bookStore.CurrentBook.Projects;


        /// <summary>
        /// Executed when the Create Project button is clicked.
        /// </summary>
        public ICommand CreateProjectButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Project Card's Delete button is
        /// clicked.
        /// </summary>
        public ICommand ProjectCardDeleteButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Project Card's Edit button is clicked.
        /// </summary>
        public ICommand ProjectCardEditButtonClickedCommand { get; }

        /// <summary>
        /// Executed when the Project Card's Name hyperlink is
        /// clicked.
        /// </summary>
        public ICommand ProjectCardNameHyperlinkClickedCommand { get; }



        public ProjectsViewModel(BookStore bookStore, NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            CreateProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateProjectButtonClicked));
            ProjectCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardDeleteButtonClicked));
            ProjectCardEditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardEditButtonClicked));
            ProjectCardNameHyperlinkClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardNameHyperlinkClicked));

            _projectDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "project details",
                    _bookStore,
                    _navigationStore);
        }


        /// <summary>
        /// Creates a new <see cref="Project"/> and adds it to the
        /// current book's <see cref="Book.Projects"/> collection.
        /// </summary>
        /// <param name="dlg"></param>
        private void CreateNewProjectRequested(CreateNewProjectDialog dlg)
        {
            // Create a new Project from the dialog inputs.
            ProjectDTO dto = new()
            {
                Code = dlg.CodeText,
                Name = dlg.NameText,
                Wbs = dlg.WbsText,
            };

            // If the project could not be created or added, display
            // an error message.
            (bool result, string? detail) =
                ProjectService.CreateNewProjectInCurrentBook(_bookStore, dto);
            if (result == false)
            {
                string caption = "Unable to Create Project";
                string message = $"The selected {detail} is assigned to another " +
                    $"project. Projects' {detail} must be unique.";
                DialogService.PromptUserWithErrorMessageWithOKButtonDialog(
                    caption, message);
            }
        }

        /// <summary>
        /// Handles the Create Project button clicked event.
        /// </summary>
        /// <param name="obj"></param>
        private void OnCreateProjectButtonClicked(object? obj)
        {
            CreateNewProjectDialog dlg =
                DialogService.PromptUserWithCreateNewProjectDialog();

            if (dlg.DialogResult == true)
            {
                CreateNewProjectRequested(dlg);
            }
        }


        private void OnProjectCardDeleteButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }


        private void OnProjectCardEditButtonClicked(object? obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Project Card's Name hyperlink click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void OnProjectCardNameHyperlinkClicked(object? obj)
        {
            Project? selectedProject = obj as Project;
            if (selectedProject == null)
            {
                throw new ArgumentOutOfRangeException("The caller must be " +
                    "a Project object");
            }

            // Set the book store's currently focused project to the
            // selected project.
            BookService.SetBookStoreCurrentFocusedProject(_bookStore, selectedProject);

            _projectDetailsNavigationService.Navigate();
        }
    }
}
