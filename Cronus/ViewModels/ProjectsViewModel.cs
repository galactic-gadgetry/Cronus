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



        public ProjectsViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;

            CreateProjectButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnCreateProjectButtonClicked));
            ProjectCardDeleteButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardDeleteButtonClicked));
            ProjectCardEditButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardEditButtonClicked));
            ProjectCardNameHyperlinkClickedCommand = new RelayCommand(
                new Action<object?>(OnProjectCardNameHyperlinkClicked));
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


        private void OnProjectCardNameHyperlinkClicked(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
