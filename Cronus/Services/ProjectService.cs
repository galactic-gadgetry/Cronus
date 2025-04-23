using System;
using System.Collections.Generic;
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
    }
}
