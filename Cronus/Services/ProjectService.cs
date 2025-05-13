using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Stores;

namespace Cronus.Services
{
    public static class ProjectService
    {
        
        public static Project CreateNewProject(ProjectDTO dto)
        {
            Project project = new()
            {
                Code = dto.Code,
                IsUnassignedProject = dto.IsUnassignedProject,
                Name = dto.Name,
                Wbs = dto.Wbs,
            };

            return project;
        }

        /// <summary>
        /// Creates a new <see cref="Project"/> instance and adds it
        /// to the current book's <see cref="Book.Projects"/>
        /// collection.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="dto">Project data transfer object used to
        /// initialize the project</param>
        /// <returns>True if the new project was added to the current
        /// book's Projects collection, false otherwise</returns>
        public static (bool, string?) CreateNewProjectInCurrentBook(
            BookStore bookStore, ProjectDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Create the new project from the DTO.
            Project project = CreateNewProject(dto);

            // Add the project to the current book's Projects
            // collection.
            return BookService.AddProjectToCurrentBook(bookStore, project);
        }

        
        public static (bool, string?) EditProjectDetails(
            BookStore bookStore, Project project, ProjectDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(project, nameof(project));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Validate that the DTO's details will be valid within
            // the current book's Projects collection, as the
            // project's details will be changed to that of the
            // DTO's.
            // A copy of the list is created and the project that
            // is to be edited is removed so that the DTO's details
            // aren't checked against the project that is wanting
            // to be changed.
            Book book = bookStore.CurrentBook;
            ObservableCollection<Project> projectsCopy =
                new ObservableCollection<Project>(book.Projects);
            projectsCopy.Remove(project);
            (bool result, string? detail) =
                ValidateProjectUniqueInCollection(projectsCopy, dto);

            // If the ValidateProjectUniqueInCollection method
            // returns false, the project details are not
            // valid, therefore return false.
            if (!result)
            {
                return (false, detail);
            }

            // Update project details.
            project.Code = dto.Code;
            project.Name = dto.Name;
            project.Wbs = dto.Wbs;

            // Set the book's HasUnsavedChanges property to
            // indicate that the book has changed.
            BookService.SetCurrentBookHasUnsavedChanges(bookStore, true);

            return (true, detail);
        }

        /// <summary>
        /// Returns a new "Unassigned" project.
        /// </summary>
        /// <returns></returns>
        public static Project GetNewUnassignedProject()
        {
            ProjectDTO dto = new()
            {
                Code = "UNSN",
                IsUnassignedProject = true,
                Name = "Unassigned",
                Wbs = "00000.00.00.00.00"
            };

            return CreateNewProject(dto);
        }

        /// <summary>
        /// Validates the project DTO's properties that are required
        /// to be unique.
        /// </summary>
        /// <param name="collection">Collection against which the
        /// project will be compared</param>
        /// <param name="dto"></param>
        /// <returns>True if the DTO is valid, false
        /// otherwise</returns>
        public static (bool, string?) ValidateProjectUniqueInCollection(
            IEnumerable<Project> collection, ProjectDTO dto)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Project project = CreateNewProject(dto);

            return ValidateProjectUniqueInCollection(collection, project);
        }

        /// <summary>
        /// Validates the project's properties that are required
        /// to be unique.
        /// </summary>
        /// <param name="collection">Collection against which the
        /// project will be compared</param>
        /// <param name="project"></param>
        /// <returns>True if the project is valid, false
        /// otherwise</returns>
        public static (bool, string?) ValidateProjectUniqueInCollection(
            IEnumerable<Project> collection, Project project)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            // If any details in any of the collection's existing
            // projects conflict with the passed project, return
            // false.
            foreach (Project p in collection)
            {
                (bool result, string? detail) = p.ContainsDetailConflict(project);
                if (result)
                {
                    return (false, detail);
                }
            }

            return (true, null);
        }
    }
}
