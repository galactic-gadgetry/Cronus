using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
        /// Used to navigate to the Edit Project Details view.
        /// </summary>
        private readonly INavigate _editProjectDetailsNavigationService;

        /// <summary>
        /// Used to determine the app's navigation state.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// Used to navigate to the Project Details view.
        /// </summary>
        private readonly INavigate _projectDetailsNavigationService;

        /// <summary>
        /// Returns the book store's current <see cref="Book"/>.
        /// </summary>
        public Book CurrentBook => _bookStore.CurrentBook;


        /// <summary>
        /// Returns the current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        public ObservableCollection<Project> Projects =>
            CurrentBook.Projects;

        /// <summary>
        /// Collection of project statistics instances for the
        /// current book.
        /// </summary>
        public ObservableCollection<ProjectStatistic> ProjectStatistics { get; set; } = new();


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


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="ProjectsViewModel"/> class.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="navigationStore"></param>
        public ProjectsViewModel(BookStore bookStore, NavigationStore navigationStore)
        {
            _bookStore = bookStore;
            _navigationStore = navigationStore;

            // Set the project statistics for each project.
            SetProjectStatistics();

            CreateProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateProjectButtonClicked));
            ProjectCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardDeleteButtonClicked));
            ProjectCardEditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardEditButtonClicked));
            ProjectCardNameHyperlinkClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardNameHyperlinkClicked));

            _editProjectDetailsNavigationService =
                ServiceFactory.CreateNavigationService(
                    "edit project details",
                    _bookStore,
                    _navigationStore);
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
            else
            {
                SetProjectStatistics();
                OnInfoUpdated($"New project '{dto.Name}' created");
            }
        }

        /// <summary>
        /// Handles the delete project request.
        /// </summary>
        /// <param name="project">Project instance to be
        /// deleted</param>
        private void DeleteProjectRequested(Project project)
        {
            BookService.DeleteProjectFromCurrentBook(_bookStore, project);
            SetProjectStatistics();

            //  Update the info bar.
            OnInfoUpdated($"Project '{project.Name}' deleted");
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

        /// <summary>
        /// Handles the Project Card's Delete button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void OnProjectCardDeleteButtonClicked(object? obj)
        {
            Project? selectedProject;
            ProjectStatistic? selectedProjectStatistic = obj as ProjectStatistic;
            if (selectedProjectStatistic == null)
            {
                throw new ArgumentOutOfRangeException("The caller must be " +
                    "a ProjectStatistic object");
            }
            else
            {
                selectedProject = Projects.FirstOrDefault(p => p.ID == selectedProjectStatistic.ID);
                if (selectedProject == null)
                {
                    throw new ArgumentOutOfRangeException("The " +
                        "Project instance could not be found in the " +
                        "collection");
                }
            }

            if (DialogService.PromptUserWithDeleteConfirmationDialog(
                selectedProject).DialogResult == true)
            {
                DeleteProjectRequested(selectedProject);
            }
        }

        /// <summary>
        /// Handles the Project Card Edit button click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void OnProjectCardEditButtonClicked(object? obj)
        {
            Project? selectedProject;
            ProjectStatistic? selectedProjectStatistic = obj as ProjectStatistic;
            if (selectedProjectStatistic == null)
            {
                throw new ArgumentOutOfRangeException("The caller must be " +
                    "a ProjectStatistic object");
            }
            else
            {
                selectedProject = Projects.FirstOrDefault(p => p.ID == selectedProjectStatistic.ID);
                if (selectedProject == null)
                {
                    throw new ArgumentOutOfRangeException("The " +
                        "Project instance could not be found in the " +
                        "collection");
                }
            }

            // Set the selected project  as the current book's
            // focused project and navigate to the Edit Project
            // Details view.
            BookService.SetBookStoreCurrentFocusedProject(_bookStore, selectedProject);
            _editProjectDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Handles the Project Card's Name hyperlink click event.
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void OnProjectCardNameHyperlinkClicked(object? obj)
        {
            Project? selectedProject;
            ProjectStatistic? selectedProjectStatistic = obj as ProjectStatistic;
            if (selectedProjectStatistic == null)
            {
                throw new ArgumentOutOfRangeException("The caller must be " +
                    "a ProjectStatistic object");
            }
            else
            {
                selectedProject = Projects.FirstOrDefault(p => p.ID == selectedProjectStatistic.ID);
                if (selectedProject == null)
                {
                    throw new ArgumentOutOfRangeException("The " +
                        "Project instance could not be found in the " +
                        "collection");
                }
            }

            // Set the book store's currently focused project to the
            // selected project.
            BookService.SetBookStoreCurrentFocusedProject(_bookStore, selectedProject);
            _projectDetailsNavigationService.Navigate();
        }

        /// <summary>
        /// Sets the properties needed for project statistics.
        /// </summary>
        private void SetProjectStatistics()
        {
            // Reset the collection.
            ProjectStatistics = new();

            TimeSpan totalTimeSpan = TimeSpan.Zero;
            int timeEntryCount = 0;

            foreach (Project p in Projects)
            {
                // Get the total duration of the time entries for the
                // project.
                TimeSpan timeSpan = TimeSpan.Zero;
                List<TimeEntry> timeEntries = CurrentBook.TimeEntries.
                    Where(t => t.AssignedProject.ID == p.ID).ToList();
                timeEntryCount = timeEntries.Count;
                foreach (TimeEntry t in timeEntries)
                {
                    timeSpan += t.Duration;
                }

                // Create a new ProjectStatistic instance for
                // displaying the statistic.
                ProjectStatistic statistic = new(p)
                {
                    Duration = timeSpan,
                    TimeEntriesCount = timeEntryCount,
                };

                // Add the ProjectStatistics to the collection.
                ProjectStatistics.Add(statistic);
            }

            OnPropertyChanged(nameof(ProjectStatistics));
        }
    }
}
